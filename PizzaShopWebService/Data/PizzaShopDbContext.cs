using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Data
{
	public class PizzaShopDbContext : DbContext
	{
		public DbSet<Manager> Managers { get; set; }

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Transaction> Transactions { get; set; }
		
		public PizzaShopDbContext (DbContextOptions<PizzaShopDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Manager>()
				.HasKey(manager => manager.PhoneNumber);
            modelBuilder.Entity<Employee>()
                .HasKey(employee => employee.PhoneNumber);				
			modelBuilder.Entity<Customer>()
				.HasKey(customer => customer.PhoneNumber);
			modelBuilder.Entity<Transaction>()
				.HasKey(transaction => transaction.TransactionID);
		}
	}
}