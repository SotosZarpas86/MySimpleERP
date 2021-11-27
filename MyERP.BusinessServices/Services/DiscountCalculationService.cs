using MyERP.BusinessServices.Discounts;
using MyERP.Core.Abstractions;
using MyERP.Core.Models.Resources.Orders;
using System.Collections.Generic;
using System.Linq;

namespace MyERP.BusinessServices.Services
{
    public class DiscountCalculationService : IDiscountCalculationService
    {
        public OrderResponseModel Execute(OrderRequestModel order)
        {
            order.Discounts.OrderBy(o => o.Priority).ToList();

            var listofDiscounts = new List<DiscountBaseClass>();

            for (int i = 0; i < order.Discounts.Count; i++)
            {
                switch (order.Discounts[i].TypeOfDiscount)
                {
                    case DiscountType.PriceList:
                        listofDiscounts.Add(new PriceList());
                        break;
                    case DiscountType.Promotion:
                        listofDiscounts.Add(new Promotion());
                        break;
                    case DiscountType.Coupon:
                        listofDiscounts.Add(new Coupon());
                        break;
                }
            }

            var responseDto = CalculateDiscount(order, listofDiscounts);
            return responseDto;
        }

        private OrderResponseModel CalculateDiscount(OrderRequestModel order, List<DiscountBaseClass> listofDiscounts)
        {
            var responseDto = new OrderResponseModel
            {
                InitialPrice = order.Price
            };
            var finalPrice = order.Price;

            foreach (var discount in listofDiscounts)
            {
                var placeholder = finalPrice;

                discount.FetchCalculationWeight();
                finalPrice = discount.Calculate(finalPrice);

                var discountTypeResponse = new DiscountTypeResponseModel
                {
                    DiscountedAmount = placeholder - finalPrice,
                    DiscountType = discount.DiscountType
                };
                responseDto.DiscountTypeResponses.Add(discountTypeResponse);
            }

            responseDto.FinalPrice = finalPrice;
            return responseDto;
        }
    }
}
