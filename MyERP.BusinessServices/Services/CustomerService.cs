using MyERP.Core.Abstractions;
using MyERP.Core.Entities;
using MyERP.Core.Models.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.BusinessServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task AddEmptyCustomerAsync()
        {
            await _customerRepository.AddEmptyCustomerAsync();
        }

        public async Task UpdateCustomerAsync(string customerData, Guid id)
        {
            var dict = new Dictionary<string, string>();

            var customerObject = JsonConvert.DeserializeObject<dynamic>(customerData);
            foreach (JProperty obj in customerObject)
            {
                if(!obj.Name.Equals("Id"))
                    dict.Add(obj.Name, obj.Value.ToString());
            }

           await _customerRepository.UpdateCustomerAsync(dict, id);
        }

        public async Task<List<CustomerDataChangeHistoryModel>> GetHistoryChangesByCustomer(Guid customerId)
        {
            var result = await _customerRepository.GetHistoryChangesByCustomer(customerId);
            return Map(result);
        }

        private List<CustomerDataChangeHistoryModel> Map(List<CustomerDataChangeHistory> data)
        {
            var mappedData = new List<CustomerDataChangeHistoryModel>();

            foreach (var item in data)
            {
                var obj = new CustomerDataChangeHistoryModel
                {
                    CurrentValue = item.CurrentValue,
                    PreviousValue = item.PreviousValue,
                    FieldName = item.FieldName,
                    ModifiedDate = item.ModifiedDate
                };
                mappedData.Add(obj);
            }

            return mappedData;
        }
    }
}
