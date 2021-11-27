using MyERP.Core.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.BusinessServices.Services
{
    public class DatabaseManagementService : IDatabaseManagementService
    {
        private readonly IDatabaseManagementRepository _databaseManagementRepository;

        public DatabaseManagementService(IDatabaseManagementRepository databaseManagementRepository)
        {
            _databaseManagementRepository = databaseManagementRepository;
        }

        public async Task<IEnumerable<string>> GetColumnNamesOfTable(string tableName)
        {
            return await _databaseManagementRepository.GetColumnNamesOfTable(tableName);
        }

        public void AddFieldToCustomerTable(string columnName)
        {
            _databaseManagementRepository.AddFieldToCustomerTable(columnName);
        }
    }
}
