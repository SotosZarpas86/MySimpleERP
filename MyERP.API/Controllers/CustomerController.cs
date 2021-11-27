using Microsoft.AspNetCore.Mvc;
using MyERP.Core.Abstractions;
using MyERP.Core.Models.Resources;
using System;
using System.Threading.Tasks;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(Guid customerId)
        {
            var result = await _customerService.GetByIdAsync(customerId);
            return Ok(result);
        }

        [HttpGet("{customerId}/HistoryChanges")]
        public async Task<IActionResult> GetHistoryChangesById(Guid customerId)
        {
            var result = await _customerService.GetHistoryChangesByCustomer(customerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmptyCustomer([FromBody] AddEmptyCustomerModel customer)
        {
            await _customerService.AddEmptyCustomerAsync();
            return Ok();
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(Guid customerId, [FromBody]UpdateCustomerModel customer)
        {
            await _customerService.UpdateCustomerAsync(customer.Data, customerId);
            return Ok();
        }
    }
}
