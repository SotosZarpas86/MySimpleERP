using MyERP.Core.Models.Resources.Orders;

namespace MyERP.Core.Abstractions
{
    public interface IDiscountCalculationService
    {
        public OrderResponseModel Execute(OrderRequestModel order);
    }
}
