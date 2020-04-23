using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using PizzaShopWebService.Data;
using PizzaShopWebService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PizzaShopWebService.Services
{
    public class PizzaShopDbHandler : IPizzaShopDbHandler
    {
		private readonly IConfiguration _configuration;
		private readonly PizzaShopDbContext _pizzaShopDbContext;

		private void AddEntity<T>(T entity)
		{
			StringBuilder contents;

			_pizzaShopDbContext.Add(entity);
			_pizzaShopDbContext.SaveChanges();

			contents = new StringBuilder();

			contents.AppendLine(JsonSerializer.Serialize(entity, new JsonSerializerOptions{
    				WriteIndented = true,
				}
			));

			System.IO.File.AppendAllText(_configuration["FilePath"],
				contents.ToString()
			);		
		}

        public PizzaShopDbHandler(IConfiguration configuration, PizzaShopDbContext pizzaShopDbContext)
		{
			_configuration = configuration;
			_pizzaShopDbContext = pizzaShopDbContext;
		}

		public void AddManager(Manager manager)
		{
			AddEntity(manager);
		}

		public void AddEmployee(Employee employee)
		{
			AddEntity(employee);
		}

		public void AddCustomer(Customer customer)
		{
			AddEntity(customer);
		}

		public void AddTransaction(Transaction transaction)
		{
			AddEntity(transaction);
		}

		public Manager FindManager(string phoneNumber)
		{
			Manager manager = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return manager;
			}

			manager = _pizzaShopDbContext.Managers.Find(phoneNumber);

			return manager;
		}

		public Employee FindEmployee(string phoneNumber)
		{
			Employee employee = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return employee;
			}

			employee = _pizzaShopDbContext.Employees.Find(phoneNumber);

			return employee;
		}

		public Customer FindCustomer(string phoneNumber)
		{
			Customer customer = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return customer;
			}

			customer = _pizzaShopDbContext.Customers.Find(phoneNumber);

			return customer;
		}
    }
}