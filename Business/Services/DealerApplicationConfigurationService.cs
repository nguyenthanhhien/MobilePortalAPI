using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Business.Models;
using Business.Utilities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class DealerApplicationConfigurationService : BaseService, IDealerApplicationConfigurationService
    {
        private readonly IHttpContextAccessor _accessor;
        private string DefaultUsername = "sysad";
        public DealerApplicationConfigurationService(CommonContext commonContext, IMapper mapper, IHttpContextAccessor accessor) : base(commonContext, mapper)
        {
            _accessor = accessor;
        }

        public BaseObjectManagementDTO<DealerApplicationConfigurationDTO> GetAll(SearchEngineDTO filter)
        {
            try
            {
                var dealersContext = CommonContext.DealerApplicationConfiguration.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Search))
                {
                    filter.Search = filter.Search.ToLower();
                    dealersContext = dealersContext.Where(x => x.DealerId.ToLower().Contains(filter.Search) ||
                    x.Application.Contains(filter.Search) ||
                    x.DeviceId.Contains(filter.Search) ||
                    x.DeviceDescription.Contains(filter.Search));
                }
                if (filter.FilterEngines.Count > 0)
                    dealersContext = FilterDealerApplicationConfigs(dealersContext, filter.FilterEngines);
                if (!string.IsNullOrEmpty(filter.Sort))
                {
                    var isAscSort = filter.Sort.Contains(Constant.AscSort);
                    if (filter.Sort.Contains(DealerApplicationConfigFields.DealerId.ToString()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.DealerId.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                    else if (filter.Sort.Contains(DealerApplicationConfigFields.Application.ToString().ToLower()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.Application.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                    else if (filter.Sort.Contains(DealerApplicationConfigFields.DeviceId.ToString().ToLower()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.DeviceId.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                    else if (filter.Sort.Contains(DealerApplicationConfigFields.DeviceDescription.ToString().ToLower()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.DeviceDescription.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                    else if (filter.Sort.Contains(DealerApplicationConfigFields.AllowAccess.ToString().ToLower()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.AllowAccess.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                    else if (filter.Sort.Contains(DealerApplicationConfigFields.ExpiredDateString.ToString().ToLower()))
                    {
                        dealersContext = dealersContext.Sort(filter, DealerApplicationConfigFields.ExpiredDate.ToString(), Enum.GetNames(typeof(DealerApplicationConfigFields)).ToList());
                    }
                }
                else
                {
                    dealersContext = dealersContext.OrderBy(x => x.DealerId).ThenBy(x => x.Application);
                }
                var totalItems = dealersContext.Count();
                var dealers = dealersContext.Skip(filter.StartIndex).Take(filter.PageSize).ToList();
                var dealersDTO = Mapper.Map<List<DealerApplicationConfiguration>, List<DealerApplicationConfigurationDTO>>(dealers);

                return new BaseObjectManagementDTO<DealerApplicationConfigurationDTO>
                {
                    ObjectList = dealersDTO,
                    TotalItems = totalItems
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string Add(DealerApplicationConfigurationDTO data)
        {
            try
            {
                var isDuplicateData = IsDuplicateData(data.DealerId, data.Application, data.DeviceId);
                if (isDuplicateData)
                    return "Can not add duplicate DealerId and Application";
                var insertData = Mapper.Map<DealerApplicationConfiguration>(data);
                insertData.CreationTimestamp = DateTime.Now;
                insertData.CreationEmpId = DefaultUsername;
                if (insertData.ExpiredDate.HasValue)
                {
                    insertData.ExpiredDate = insertData.ExpiredDate.Value.Date;
                }
                CommonContext.DealerApplicationConfiguration.Add(insertData);
                CommonContext.SaveChanges();
                return string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Update(DealerApplicationConfigurationDTO data)
        {
            try
            {
                var dataEntity = CommonContext.DealerApplicationConfiguration.Find(data.DealerApplicationConfigurationKey);
                if (dataEntity == null)
                    return "Dealer Application Configuration not found";
                var isDuplicateData = IsDuplicateData(data.DealerId, data.Application, data.DeviceId, data.DealerApplicationConfigurationKey);
                if (isDuplicateData)
                    return "Not allow to add duplicate DealerId and Application";
                var updateData = Mapper.Map(data, dataEntity);
                if (updateData.ExpiredDate.HasValue)
                {
                    updateData.ExpiredDate = updateData.ExpiredDate.Value.ToLocalTime();
                }
                CommonContext.DealerApplicationConfiguration.Update(updateData);
                CommonContext.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDuplicateData(string dealerId, string application, string deviceId, int? key = null)
        {
            var existedRecord = CommonContext.DealerApplicationConfiguration.FirstOrDefault(x => x.Application.ToLower() == application.ToLower() &&
                                                                                            x.DealerId.ToUpper() == dealerId.ToUpper() &&
                                                                                            x.DeviceId == deviceId &&
                                                                                            (key == null || x.DealerApplicationConfigurationKey != key));
            if (existedRecord != null)
                return true;
            return false;
        }

        public DealerApplicationConfigurationDTO GetByKey(int key)
        {
            var dataEntity = CommonContext.DealerApplicationConfiguration.Find(key);
            if (dataEntity == null)
                return null;
            return Mapper.Map<DealerApplicationConfigurationDTO>(dataEntity);

        }

        public List<string> GetDealers()
        {
            var result = new List<string>();
            var query = @"SELECT * FROM     dbo.DEALER_CONFIGURATION DC
                                    JOIN	SYS.DATABASES SD
                                    ON	    SD.NAME = DC.DATABASE_NAME
			                        AND	    SD.STATE = 0
                                    AND	    SD.IS_READ_ONLY = 0
                                    WHERE   DC.IS_TITAN_DATABASE = 1";
            return CommonContext.DealerConfiguration.FromSqlRaw(query).Select(x => x.DealerId.ToUpper()).ToList();
        }

        public IQueryable<DealerApplicationConfiguration> FilterDealerApplicationConfigs(IQueryable<DealerApplicationConfiguration> dealers, List<FilterEngineDTO> filterEngines)
        {
            Expression<Func<DealerApplicationConfiguration, bool>> expression = null;
            for (var i = 0; i < filterEngines.Count; i++)
            {
                var filterObject = GetDataByFilterObject(filterEngines[i].Filters[0]);
                if (filterEngines[i].Filters.Count > 1)
                {
                    var secondObject = GetDataByFilterObject(filterEngines[i].Filters[1]);
                    if (filterEngines[i].Logic == LogicFilters.And.ToString())
                    {
                        filterObject = filterObject.And(secondObject);
                    }
                    else
                    {
                        filterObject = filterObject.Or(secondObject);
                    }
                }
                if (i == 0)
                {
                    expression = filterObject;
                }
                else
                {
                    expression = expression.And(filterObject);
                }

            }

            return dealers.Where(expression.Compile()).AsQueryable(); ;
        }
        public Expression<Func<DealerApplicationConfiguration, bool>> GetDataByFilterObject(FilterObjectDTO filterObject)
        {
            Expression<Func<DealerApplicationConfiguration, bool>> expression = null;
            var fieldName = filterObject.Field;
            var compareValue = filterObject.Value.ToString();
            if (fieldName == DealerApplicationConfigFields.AllowAccess.ToString())
            {
                compareValue = Convert.ToBoolean(filterObject.Value.ToString()) ? "1" : "0";
            }
            else if (fieldName == DealerApplicationConfigFields.OriginalApplication.ToString())
            {
                fieldName = DealerApplicationConfigFields.Application.ToString();
            }

            switch (filterObject.Operator)
            {
                case Constant.EqualToFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.NotEqualToFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeNotEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringNotEqual<DealerApplicationConfiguration>(fieldName, compareValue);

                    }
                    break;
                case Constant.EqualToNullFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeIsNull<DealerApplicationConfiguration>(fieldName);
                        else
                            expression = ExpressionUtility.WhereStringIsNull<DealerApplicationConfiguration>(fieldName);
                    }
                    break;
                case Constant.NotEqualToNullFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeIsNotNull<DealerApplicationConfiguration>(fieldName);
                        else
                            expression = ExpressionUtility.WhereStringIsNotNull<DealerApplicationConfiguration>(fieldName);
                    }
                    break;
                case Constant.LessThanFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeLessThan<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringLessThan<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.LessThanOrEqualToFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeLessThanOrEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringLessThanOrEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.GreaterThanFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeGreaterThan<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringGreaterThan<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.GreaterThanOrEqualToFilter:
                    {
                        if (fieldName == DealerApplicationConfigFields.ExpiredDate.ToString())
                            expression = ExpressionUtility.WhereDateTimeGreaterThanOrEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                        else
                            expression = ExpressionUtility.WhereStringGreaterThanOrEqual<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.StartsWithFilter:
                    {
                        expression = ExpressionUtility.WhereStringStartsWith<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.EndsWithFilter:
                    {
                        expression = ExpressionUtility.WhereStringEndsWith<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.ContainsToFilter:
                    {
                        expression = ExpressionUtility.WhereStringContains<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.DoesNotContainFilter:
                    {
                        expression = ExpressionUtility.WhereStringNotContains<DealerApplicationConfiguration>(fieldName, compareValue);
                    }
                    break;
                case Constant.IsEmptyFilter:
                    {
                        expression = ExpressionUtility.WhereStringEmpty<DealerApplicationConfiguration>(fieldName);
                    }
                    break;
                case Constant.IsNotEmptyFilter:
                    {
                        expression = ExpressionUtility.WhereStringNotEmpty<DealerApplicationConfiguration>(fieldName);
                    }
                    break;
            }
            return expression;
        }
    }
}
