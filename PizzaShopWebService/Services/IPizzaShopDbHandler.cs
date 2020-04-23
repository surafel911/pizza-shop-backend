using System.Collections.Generic;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopDbHandler
    {
		void AddManager(Manager manager);
		void AddEmployee(Employee employee);
		void AddCustomer(Customer customer);
		void AddTransaction(Transaction transaction);

		Manager FindManager(string phoneNumber);
		Employee FindEmployee(string phoneNumber);
        Customer FindCustomer(string phoneNumber);
    }
}