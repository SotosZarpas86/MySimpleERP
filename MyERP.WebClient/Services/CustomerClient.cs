using MyERP.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyERP.WebClient.Services
{
    public class CustomerClient
    {
        public HttpClient Client { get; }

        public CustomerClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://localhost:44352/api/");
            Client.Timeout = new TimeSpan(0, 0, 30);
            Client.DefaultRequestHeaders.Clear();
        }

        public async Task<string> GetAllAsync()
        {
            using (var response = await Client.GetAsync("customer", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            using (var response = await Client.GetAsync("customer/" + id, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<List<CustomerDataChangeHistoryViewModel>> GetHistoryChangesById(Guid id)
        {
            using (var response = await Client.GetAsync("customer/" + id + "/HistoryChanges", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<CustomerDataChangeHistoryViewModel>>(result, options);
            }
        }

        public async Task UpdateAsync(string input, Guid customerId)
        {
            var updateCustomer = new UpdateCustomerViewModel
            {
                Data = input
            };
            var updateCustomerJson = new StringContent(JsonSerializer.Serialize(updateCustomer), Encoding.UTF8, "application/json");

            using (var response = await Client.PutAsync($"customer/{customerId}", updateCustomerJson))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task AddCustomerAsync()
        {
            var addEmptyCustomer = new AddEmptyCustomerViewModel();
            var addEmptyCustomerJson = new StringContent(JsonSerializer.Serialize(addEmptyCustomer), Encoding.UTF8, "application/json");

            using (var response = await Client.PostAsync($"customer/", addEmptyCustomerJson))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
