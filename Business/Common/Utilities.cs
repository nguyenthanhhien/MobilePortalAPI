using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Business
{
    public class Utilities
    {

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

    }
}
