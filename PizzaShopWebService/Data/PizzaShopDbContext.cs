using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Data
{
	public class PizzaShopDbContext : DbContext
	{
		public DbSet<ManagerDTO> Managers { get; set; }

		public DbSet<EmployeeDTO> Employees { get; set; }

		public DbSet<CustomerDTO> Customers { get; set; }

		public DbSet<TransactionDTO> Transactions { get; set; }
		
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