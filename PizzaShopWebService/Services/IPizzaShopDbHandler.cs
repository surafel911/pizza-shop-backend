using System.Threading.Tasks;
using System.Collections.Generic;


using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopDbHandler
    {
		void AddManager(ManagerDTO managerDTO);
		void AddEmployee(EmployeeDTO employeeDTO);
		void AddCustomer(CustomerDTO customerDTO);
		void AddTransaction(TransactionDTO transactionDTO);

		void UpdateCustomer(CustomerDTO customerDTO);

		ManagerDTO FindManager(string phoneNumber);
		EmployeeDTO FindEmployee(string phoneNumber);
        CustomerDTO FindCustomer(string phoneNumber);

		Task<List<TransactionDTO>> GetTransactions();
    }
}