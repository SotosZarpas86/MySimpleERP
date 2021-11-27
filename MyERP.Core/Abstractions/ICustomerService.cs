using MyERP.Core.Models.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.Core.Abstractions
{
    public interface ICustomerService
    {
        Task<string> GetAllAsync();

        Task<string> GetByIdAsync(Guid id);

        Task AddEmptyCustomerAsync();

        Task UpdateCustomerAsync(string customerData, Guid id);

        Task<List<CustomerDataChangeHistoryModel>> GetHistoryChangesByCustomer(Guid customerId);
    }
}
