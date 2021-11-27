using System;
using System.ComponentModel.DataAnnotations;

namespace MyERP.Core.Entities
{
    public class CustomerDataChangeHistory
    {
        [Key]
        public int Id { get; set; }

        public string FieldName { get; set; }

        public string PreviousValue { get; set; }

        public string CurrentValue { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid CustomerId { get; set; }
    }
}
