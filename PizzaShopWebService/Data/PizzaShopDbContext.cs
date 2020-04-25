using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Data
{
	public class PizzaShopDbContext : DbContext
	{
		public DbSet<ManagerDTO> Managers { get; }

		public DbSet<EmployeeDTO> Employees { get; }

		public DbSet<CustomerDTO> Customers { get; }

		public DbSet<TransactionDTO> Transactions { get; }
		
		public PizzaShopDbContext (DbContextOptions<PizzaShopDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ManagerDTO>()
				.HasKey(manager => manager.PhoneNumber);
            modelBuilder.Entity<EmployeeDTO>()
                .HasKey(employee => employee.PhoneNumber);				
			modelBuilder.Entity<CustomerDTO>()
				.HasKey(customer => customer.PhoneNumber);
			modelBuilder.Entity<TransactionDTO>()
				.HasKey(transaction => transaction.TransactionID);
		}
	}
}