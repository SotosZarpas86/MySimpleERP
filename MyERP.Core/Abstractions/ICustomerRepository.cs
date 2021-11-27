using MyERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.Core.Abstractions
{
    public interface ICustomerRepository
    {
        Task<string> GetAllAsync();

        Task<string> GetByIdAsync(Guid id);

        Task AddEmptyCustomerAsync();

        Task UpdateCustomerAsync(Dictionary<string, string> customerData, Guid customerId);

        Task<List<CustomerDataChangeHistory>> GetHistoryChangesByCustomer(Guid customerId);
    }
}
