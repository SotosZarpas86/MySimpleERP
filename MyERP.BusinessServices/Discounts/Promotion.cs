using MyERP.Core.Models.Resources.Orders;

namespace MyERP.BusinessServices.Discounts
{
    public class Promotion : DiscountBaseClass
    {
        public double PromotionDiscount { get; set; }

        public override DiscountType DiscountType { get; set; } = DiscountType.Promotion;

        public override double Calculate(double productPrice)
        {
            return productPrice - (productPrice * PromotionDiscount);
        }

        public override double FetchCalculationWeight()
        {
            return PromotionDiscount = 0.10;
        }
    }
}
