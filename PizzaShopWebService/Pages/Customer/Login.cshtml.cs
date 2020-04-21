using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class LoginModel : PageModel   
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[Required]
		[BindProperty]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Required]
		[BindProperty]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string ErrorMessage { get; private set; }

		public LoginModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
     			return Page();
    		}

			Customer customer = _pizzaShopDbHandler.FindCustomer(PhoneNumber);
			if (customer == null) {
				ErrorMessage = "This phone number isn't registered to an account. Please sign up.";
				return Page();
			}

			Employee employee = _pizzaShopDbHandler.FindEmployee(PhoneNumber);
			if (employee != null) {
				ErrorMessage = "This phone number is registered with an employee account. Please use a differnet phone number.";
				return Page();
			}

			Manager manager = _pizzaShopDbHandler.FindManager(PhoneNumber);
			if (manager != null) {
				ErrorMessage = "This phone number is registered with a manager account. Please use a differnet phone number.";
				return Page();
			}

			HttpContext.Session.SetString("PhoneNumber", PhoneNumber);

			return RedirectToPage("/Menu");
		}
    }
}
    