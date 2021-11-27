using System.Text.Json.Serialization;

namespace MyERP.Core.Models.Resources.Orders
{
    public class DiscountTypeResponseModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiscountType DiscountType { get; set; }

        public double DiscountedAmount { get; set; }
    }
}
