using Microsoft.AspNetCore.Mvc;
using MyERP.WebClient.Models;
using MyERP.WebClient.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyERP.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseManagementClient _databaseManagementClient;

        public HomeController(DatabaseManagementClient databaseManagementClient)
        {
            _databaseManagementClient = databaseManagementClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _databaseManagementClient.GetColumnNamesOfTableAsync("Customers");
            var customerModel = new CustomerViewModel
            {
                AvailableColumns = response
            };
            return View(customerModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddColumn(CustomerViewModel model)
        {
            await _databaseManagementClient.AddColumnAsync(model.Column);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
