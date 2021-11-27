using Microsoft.AspNetCore.Mvc;
using MyERP.WebClient.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.WebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerClient _customerClient;

        public CustomerController(CustomerClient customerClient)
        {
            _customerClient = customerClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var response = await _customerClient.GetAllAsync();
            return new JsonResult(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(Guid id)
        {
            var response = await _customerClient.GetByIdAsync(id);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Add()
        {
            await _customerClient.AddCustomerAsync();
            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryChangesByCustomer(Guid id)
        {
            var result = await _customerClient.GetHistoryChangesById(id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] string result)
        {
            var jsonMessage = CreateJsonMessage(result);

            var customerId = GetCustomerId(result);

            await _customerClient.UpdateAsync(jsonMessage, customerId);

            return new JsonResult(true);
        }

        #region private methods

        private string CreateJsonMessage(string input)
        {
            var json = "{ ";
            var temp = input.Split("&").ToList();
            foreach (var item in temp.Where(a => !a.Contains("__RequestVerificationToken")))
            {
                var temp2 = item.Split("=");
                json += $@" ""{temp2[0]}"" : ""{temp2[1]}"",";
            }

            json = json.Remove(json.Length - 1, 1);

            json = json + " }";

            return json;
        }

        private Guid GetCustomerId(string input)
        {
            var temp = input.Split("&");
            foreach (var item in temp)
            {
                var temp2 = item.Split("=");
                foreach (var item2 in temp2)
                {
                    if (item2.Equals("Id"))
                    {
                        return Guid.Parse(temp2[1]);
                    }
                }
            }
            return Guid.Empty;
        }

        #endregion
    }
}
