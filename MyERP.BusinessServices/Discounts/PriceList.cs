using MyERP.Core.Models.Resources.Orders;

namespace MyERP.BusinessServices.Discounts
{
    public class PriceList : DiscountBaseClass
    {
        public double PriceListDiscount { get; set; }

        public override DiscountType DiscountType { get; set; } = DiscountType.PriceList;

        public override double Calculate(double productPrice)
        {
            return productPrice - (productPrice * PriceListDiscount);
        }

        public override double FetchCalculationWeight()
        {
            return PriceListDiscount = 0.05;
        }
    }
}
