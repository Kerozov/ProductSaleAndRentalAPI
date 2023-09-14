using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Persistence.Migrations;

 public static class DatabaseCreate
    {
        public static void Create(IConfiguration configuration)
        {
            var rentNBuyConnectionString = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
            string dbName = rentNBuyConnectionString.InitialCatalog;

            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using var connection = new SqlConnection(configuration.GetConnectionString("SqlConnectionMigration"));
            var records = connection.Query("SELECT * FROM sys.databases WHERE name = @name",
                parameters);

            if (records.Any() == false)
            {
                connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
