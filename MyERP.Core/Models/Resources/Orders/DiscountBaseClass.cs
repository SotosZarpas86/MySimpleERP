namespace MyERP.Core.Models.Resources.Orders
{
    public abstract class DiscountBaseClass
    {
        public abstract double Calculate(double productPrice);

        public abstract double FetchCalculationWeight();

        public abstract DiscountType DiscountType { get; set; }
    }
}
