using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyERP.Core.Abstractions;
using MyERP.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        public readonly MyERPContext _myERPContext;
        private readonly IConfiguration _configuration;
        private static string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public CustomerRepository(MyERPContext myERPContext,
                                  IConfiguration configuration)
        {
            _myERPContext = myERPContext;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyERPDBConnection");
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public async Task<string> GetAllAsync()
        {
            try
            {
                var selectQuery = "SELECT * FROM Customers";
                var result = await _sqlConnection.QueryAsync(selectQuery);

                var customersList = (from row in result
                                     select (IDictionary<string, object>)row).AsList();

                return JsonConvert.SerializeObject(customersList, Formatting.Indented);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            try
            {
                var selectQuery = "SELECT * FROM Customers WHERE Id = @Id";
                var result = await _sqlConnection.QueryAsync(selectQuery, new { Id = id });

                var customer = (from row in result
                                select (IDictionary<string, object>)row).AsList().FirstOrDefault();

                return JsonConvert.SerializeObject(customer, Formatting.Indented);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddEmptyCustomerAsync()
        {
            var customer = new Customer();
            _myERPContext.Customers.Add(customer);
            await _myERPContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Dictionary<string, string> customerData, Guid customerId)
        {
            var dataTable = await CreateHistoryRecordDataTable(customerId, customerData);

            var updateQuery = "UPDATE [dbo].[Customers] SET ";

            foreach (var item in customerData)
            {
                updateQuery += $" {item.Key} = '{item.Value}',";
            }

            updateQuery = updateQuery.Remove(updateQuery.Length - 1, 1); //Remove last comma from query

            updateQuery = updateQuery + "WHERE Id = @Id";
            _sqlConnection.Open();

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    await _sqlConnection.ExecuteAsync(updateQuery, new { Id = customerId }, transaction);
                    var bulk = CreateSqlBulkCopy(transaction);

                    bulk.WriteToServer(dataTable);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

        }

        public async Task<List<CustomerDataChangeHistory>> GetHistoryChangesByCustomer(Guid customerId)
        {
            return await _myERPContext.CustomerDataChangeHistory.Where(c => c.CustomerId == customerId).ToListAsync();
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        private SqlBulkCopy CreateSqlBulkCopy(SqlTransaction transaction)
        {
            var bulk = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.Default, transaction)
            {

                DestinationTableName = "CustomerDataChangeHistory"
            };
            bulk.ColumnMappings.Add("FieldName", "FieldName");
            bulk.ColumnMappings.Add("PreviousValue", "PreviousValue");
            bulk.ColumnMappings.Add("CurrentValue", "CurrentValue");
            bulk.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            bulk.ColumnMappings.Add("CustomerId", "CustomerId");
            return bulk;
        }

        private async Task<DataTable> CreateHistoryRecordDataTable(Guid customerId, Dictionary<string, string> newCustomerData)
        {
            var dataToUpdate = new List<Tuple<string, string, string>>();

            var selectQuery = "SELECT * FROM Customers WHERE Id = @Id";
            var result = await _sqlConnection.QueryAsync(selectQuery, new { Id = customerId });

            var currentCustomerData = (from row in result
                                       select (IDictionary<string, object>)row).AsList().FirstOrDefault();

            foreach (var item in currentCustomerData.Where(a => a.Key != "Id"))
            {
                newCustomerData.TryGetValue(item.Key, out string newValue);

                if (item.Value?.ToString() != newValue)
                {
                    var tuple = new Tuple<string, string, string>(item.Key, item.Value?.ToString(), newValue);
                    dataToUpdate.Add(tuple);
                }
            }



            var dataTable = new DataTable();
            dataTable.Columns.Add("FieldName", typeof(string));
            dataTable.Columns.Add("PreviousValue", typeof(string));
            dataTable.Columns.Add("CurrentValue", typeof(string));
            dataTable.Columns.Add("ModifiedDate", typeof(DateTime));
            dataTable.Columns.Add("CustomerId", typeof(Guid));

            foreach (var item in dataToUpdate)
            {
                dataTable.Rows.Add(item.Item1, item.Item2, item.Item3, DateTime.Now, customerId);
            }
            return dataTable;
        }
    }
}
