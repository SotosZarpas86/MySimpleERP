using Microsoft.EntityFrameworkCore;
using MyERP.Core.Entities;

namespace MyERP.Infrastructure
{
    public class MyERPContext : DbContext
    {
        public MyERPContext(DbContextOptions<MyERPContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerDataChangeHistory> CustomerDataChangeHistory { get; set; }
    }
}
