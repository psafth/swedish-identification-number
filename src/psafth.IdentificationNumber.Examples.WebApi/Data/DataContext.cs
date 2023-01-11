using psafth.IdentificationNumber.Swedish.Extensions;
using Microsoft.EntityFrameworkCore;
using psafth.IdentificationNumber.Examples.WebApi.Models;

namespace psafth.IdentificationNumber.Examples.WebApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .Property(e => e.IdentificationNumber)
                .HasConversion(
                    v => v.ToString(),
                    v => v.ToIdentificationNumber());
        }
    }
}
