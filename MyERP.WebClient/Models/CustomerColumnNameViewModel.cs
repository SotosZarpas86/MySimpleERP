using System.ComponentModel.DataAnnotations;

namespace MyERP.WebClient.Models
{
    public class CustomerColumnNameViewModel
    {
        [Required]
        public string ColumnName { get; set; }
    }
}
