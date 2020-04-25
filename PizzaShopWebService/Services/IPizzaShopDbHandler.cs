using System.Collections.Generic;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopDbHandler
    {
		void AddManager(ManagerDTO manager);
		void AddEmployee(EmployeeDTO employee);
		void AddCustomer(CustomerDTO customer);
		void AddTransaction(TransactionDTO transaction);

		ManagerDTO FindManager(string phoneNumber);
		EmployeeDTO FindEmployee(string phoneNumber);
        CustomerDTO FindCustomer(string phoneNumber);
    }
}