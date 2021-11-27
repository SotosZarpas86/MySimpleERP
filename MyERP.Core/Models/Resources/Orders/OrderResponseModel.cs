using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Models.Resources.Orders
{
    public class OrderResponseModel
    {
        public double InitialPrice { get; set; }

        public double FinalPrice { get; set; }

        public List<DiscountTypeResponseModel> DiscountTypeResponses { get; set; } = new List<DiscountTypeResponseModel>();
    }
}
