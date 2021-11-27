using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.Core.Abstractions
{
    public interface IDatabaseManagementRepository
    {
        Task<IEnumerable<string>> GetColumnNamesOfTable(string tableName);
        void AddFieldToCustomerTable(string columnName);
    }
}
