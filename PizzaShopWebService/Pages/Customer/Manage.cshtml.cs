using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Customer
{
    public class ManageModel : PageModel   
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty]
        public CustomerDTO Customer { get; set; }

		public string ErrorMessage { get; private set; }

		public ManageModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			if (_pizzaShopDbHandler.FindManager(Customer.PhoneNumber) != null ||
				_pizzaShopDbHandler.FindEmployee(Customer.PhoneNumber) != null ||
				_pizzaShopDbHandler.FindCustomer(Customer.PhoneNumber) != null) {
				ErrorMessage = "This phone number is registered with another account. Please try a different phone number.";
				return Page();
			}

			_pizzaShopDbHandler.AddCustomer(Customer);

			return RedirectToPage("/Customer/Login");
		}
	}
}