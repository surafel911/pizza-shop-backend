using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Data
{
    public class PizzaShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
        public PizzaShopDbContext (DbContextOptions<PizzaShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.PhoneNumber);
            modelBuilder.Entity<Transaction>()
                .HasKey(transaction => transaction.TransactionID);
            modelBuilder.Entity<Manager>()
                .HasKey(manager => manager.PhoneNumber);
        }
    }
}