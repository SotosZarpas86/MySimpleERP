using MyERP.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyERP.WebClient.Services
{
    public class DatabaseManagementClient
    {
        public HttpClient Client { get; }

        public DatabaseManagementClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://localhost:44352/api/");
            Client.Timeout = new TimeSpan(0, 0, 30);
            Client.DefaultRequestHeaders.Clear();
        }

        public async Task<List<string>> GetColumnNamesOfTableAsync(string tableName)
        {
            try
            {
                using (var response = await Client.GetAsync("DatabaseManagement/" + tableName + "/GetColumns", HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<string>>(result);
                }
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
   
        }

        public async Task AddColumnAsync(CustomerColumnNameViewModel model)
        {
            var request = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            using (var response = await Client.PostAsync("DatabaseManagement/", request))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
