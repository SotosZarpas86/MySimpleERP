using System.ComponentModel.DataAnnotations;

namespace MyERP.Core.Models.Resources
{
    public class AlterTableModel
    {
        [Required]
        public string ColumnName { get; set; }
    }
}
