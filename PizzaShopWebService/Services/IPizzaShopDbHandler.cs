using System.Threading.Tasks;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopDbHandler
    {
		void AddManager(Manager manager);
		void AddEmployee(Employee employee);
		void AddCustomer(Customer customer);

		Manager FindManager(string phoneNumber);
		Employee FindEmployee(string phoneNumber);
        Customer FindCustomer(string phoneNumber);
    }
}