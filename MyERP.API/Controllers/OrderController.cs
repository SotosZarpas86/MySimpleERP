using Microsoft.AspNetCore.Mvc;
using MyERP.Core.Abstractions;
using MyERP.Core.Models.Resources.Orders;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDiscountCalculationService _discountCalculationService;

        public OrderController(IDiscountCalculationService discountCalculationService)
        {
            _discountCalculationService = discountCalculationService;
        }

        [HttpPost("discount")]
        public IActionResult CalculateDiscount([FromBody] OrderRequestModel order)
        {
            var response = _discountCalculationService.Execute(order);
            return Ok(response);
        }
    }
}
