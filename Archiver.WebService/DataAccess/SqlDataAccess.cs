using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Archiver.WebService.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectioName = "ArchiverDB")
        {
            return ConfigurationManager.ConnectionStrings[connectioName].ConnectionString;
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {
                return dbConnection.Execute(sql, data);
            }
        }
    }
}
