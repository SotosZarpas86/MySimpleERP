using System.Collections.Generic;

namespace MyERP.Core.Models.Resources.Orders
{
    public class OrderRequestModel
    {
        public string ProductName { get; set; }

        public double Price { get; set; }

        public List<DiscountModel> Discounts { get; set; } = new List<DiscountModel>();
    }
}
