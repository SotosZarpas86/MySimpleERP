using System;
using System.ComponentModel.DataAnnotations;

namespace MyERP.Core.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
    }
}
