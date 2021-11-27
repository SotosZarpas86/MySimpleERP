using MyERP.Core.Models.Resources.Orders;

namespace MyERP.BusinessServices.Discounts
{
    public class Coupon : DiscountBaseClass
    {
        public double CouponDiscount { get; set; }

        public override DiscountType DiscountType { get; set; } = DiscountType.Coupon;

        public override double Calculate(double productPrice)
        {
            return productPrice - CouponDiscount;
        }

        public override double FetchCalculationWeight()
        {
            return CouponDiscount = 10;
        }
    }
}
