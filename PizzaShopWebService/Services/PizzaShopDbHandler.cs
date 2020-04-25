using System.Net;
using System.IO;
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

			File.AppendAllText(_configuration["FilePath"],
				contents.ToString()
			);		
		}

        public PizzaShopDbHandler(IConfiguration configuration, PizzaShopDbContext pizzaShopDbContext)
		{
			_configuration = configuration;
			_pizzaShopDbContext = pizzaShopDbContext;

			if (File.Exists(_configuration["FilePath"])) {
				File.Delete(_configuration["FilePath"]);
			}
		}

		public void AddManager(ManagerDTO manager)
		{
			AddEntity(manager);
		}

		public void AddEmployee(EmployeeDTO employee)
		{
			AddEntity(employee);
		}

		public void AddCustomer(CustomerDTO customer)
		{
			AddEntity(customer);
		}

		public void AddTransaction(TransactionDTO transaction)
		{
			AddEntity(transaction);
		}

		public ManagerDTO FindManager(string phoneNumber)
		{
			ManagerDTO manager = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return manager;
			}

			manager = _pizzaShopDbContext.Managers.Find(phoneNumber);

			return manager;
		}

		public EmployeeDTO FindEmployee(string phoneNumber)
		{
			EmployeeDTO employee = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return employee;
			}

			employee = _pizzaShopDbContext.Employees.Find(phoneNumber);

			return employee;
		}

		public CustomerDTO FindCustomer(string phoneNumber)
		{
			CustomerDTO customer = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return customer;
			}

			customer = _pizzaShopDbContext.Customers.Find(phoneNumber);

			return customer;
		}
    }
}