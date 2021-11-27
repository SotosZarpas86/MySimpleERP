using System.ComponentModel.DataAnnotations;

namespace MyERP.Core.Models.Resources
{
    public class UpdateCustomerModel
    {
        [Required]
        public string Data { get; set; }
    }
}
