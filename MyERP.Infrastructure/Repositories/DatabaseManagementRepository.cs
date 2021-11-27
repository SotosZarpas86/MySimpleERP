using Dapper;
using Microsoft.Extensions.Configuration;
using MyERP.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Infrastructure.Repositories
{
    public class DatabaseManagementRepository : IDatabaseManagementRepository, IDisposable
    {
        private readonly IConfiguration _configuration;
        private static string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public DatabaseManagementRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyERPDBConnection");
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<string>> GetColumnNamesOfTable(string tableName)
        {
            var selectQuery = @$"SELECT c.name FROM sys.columns c
                                INNER JOIN sys.tables t
                                on t.object_id = c.object_id
                                AND t.name = '{tableName}' AND t.type = 'U'";

            var result = await _sqlConnection.QueryAsync<string>(selectQuery);

            return result;
        }

        public void AddFieldToCustomerTable(string columnName)
        {
            try
            {
                var alterSqlQuery = "DECLARE @sup nvarchar(15); " +
                    "SET @sup = '" + columnName + "'; " +
                    "EXEC ('ALTER TABLE Customers ADD ' + @sup + ' nvarchar(MAX) NULL')";

                _sqlConnection.Open();

                var alterCommand = new SqlCommand(alterSqlQuery, _sqlConnection);
                alterCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }
    }
}
