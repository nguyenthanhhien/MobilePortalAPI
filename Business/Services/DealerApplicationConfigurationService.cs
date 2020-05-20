using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Business.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

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

        public BaseObjectManagementDTO<DealerApplicationConfigurationDTO> GetAll(int page, int itemPerPage, string sort, string search)
        {
            try
            {
                var offset = Utilities.CalItemOffsetForPagination(page, itemPerPage);
                var dealersContext = CommonContext.DealerApplicationConfiguration.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    dealersContext = dealersContext.Where(x => x.DealerId.ToLower().Contains(search) ||
                    x.Application.Contains(search) ||
                    x.DeviceId.Contains(search) ||
                    x.DeviceDescription.Contains(search));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = sort.ToLower();
                    var isAscSort = sort.Contains(SortTypes.Asc.ToString().ToLower()) ? true : false;
                    if (sort.Contains(DealerApplicationConfigurationSortFields.DealerId.ToString().ToLower())) {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.DealerId) : dealersContext.OrderByDescending(x => x.DealerId);
                    }
                    else if (sort.Contains(DealerApplicationConfigurationSortFields.Application.ToString().ToLower()))
                    {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.Application) : dealersContext.OrderByDescending(x => x.Application);
                    }
                    else if (sort.Contains(DealerApplicationConfigurationSortFields.DeviceId.ToString().ToLower()))
                    {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.DeviceId) : dealersContext.OrderByDescending(x => x.DeviceId);
                    }
                    else if (sort.Contains(DealerApplicationConfigurationSortFields.DeviceDescription.ToString().ToLower()))
                    {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.DeviceDescription) : dealersContext.OrderByDescending(x => x.DeviceDescription);
                    }
                    else if (sort.Contains(DealerApplicationConfigurationSortFields.IsAllowAccess.ToString().ToLower()))
                    {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.AllowAccess) : dealersContext.OrderByDescending(x => x.AllowAccess);
                    }
                    else if (sort.Contains(DealerApplicationConfigurationSortFields.ExpiredDateString.ToString().ToLower()))
                    {
                        dealersContext = isAscSort ? dealersContext.OrderBy(x => x.ExpiredDate) : dealersContext.OrderByDescending(x => x.ExpiredDate);
                    }
                }
                else
                {
                    dealersContext = dealersContext.OrderBy(x => x.DealerId).ThenBy(x => x.Application);
                }
                var totalItems = dealersContext.Count();
                var dealers = dealersContext.Skip(offset).Take(itemPerPage).ToList();
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
                    return "Not allow to add duplicate DealerId and Application";
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
            if (string.IsNullOrEmpty(deviceId))
                return true;
            var existedRecord = CommonContext.DealerApplicationConfiguration.FirstOrDefault(x => x.Application.ToLower() == application.ToLower() &&
                                                                                            x.DealerId.ToLower() == dealerId.ToLower() &&
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
    }
}
