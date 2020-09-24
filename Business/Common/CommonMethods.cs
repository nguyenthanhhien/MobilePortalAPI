using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using DTO;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Business
{
    public static class CommonMethods
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> entities, SearchEngineDTO filter,
            string defaultSortColumn, List<string> sortColumns)
        {
            try
            {
                entities = entities.OrderBy(defaultSortColumn);
                var isAscSort = true;
                if (!string.IsNullOrWhiteSpace(filter.Sort) &&
                    (filter.Sort.Contains(Constant.AscSort) || filter.Sort.Contains(Constant.DescSort)))
                {
                    isAscSort = filter.Sort.Contains(Constant.AscSort) ? true : false;

                    foreach (var column in sortColumns)
                    {
                        if (filter.Sort.Contains(column))
                        {

                            if (isAscSort)
                                entities = entities.OrderBy(column);
                            else
                                entities = entities.OrderBy(column + " descending");

                            break;
                        }
                    }
                }

                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IDbConnection CreateConnection(String connectionString)
        {
            return ((IDbConnection)new SqlConnection(connectionString));
        }
        public static string CreateCommonConnection(string commonServerName, string userId, string password)
        {
            return "Server=" + commonServerName + ";Database=Common" + ";User ID=" + userId + ";Password=" + password + ";Trusted_Connection=false;";
        }

        public static int CalItemOffsetForPagination(int page, int itemPerPage)
        {
            return (page - 1) * itemPerPage;
        }

        public static DateTime ConvertStringToDateTime(string value)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            return Convert.ToDateTime(value, dtfi).ToLocalTime();
        }
    }
}
