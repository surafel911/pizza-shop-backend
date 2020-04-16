using System.Threading.Tasks;

using PizzaShopWebService.Data;
using PizzaShopWebService.Models;

using Microsoft.EntityFrameworkCore;

namespace PizzaShopWebService.Services
{
    public class PizzaShopDbHandler : IPizzaShopDbHandler
    {
		private readonly PizzaShopDbContext _pizzaShopDbContext;

		private void AddEntity<T>(T entity)
		{
			_pizzaShopDbContext.Add(entity);
			_pizzaShopDbContext.SaveChanges();			
		}

        public PizzaShopDbHandler(PizzaShopDbContext pizzaShopDbContext)
		{
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