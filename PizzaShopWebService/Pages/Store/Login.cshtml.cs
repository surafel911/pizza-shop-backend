using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Store
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

		public void OnGet()
		{
			ViewData["Store"] = true;
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
     			return Page();
    		}

			CustomerDTO customerDTO = _pizzaShopDbHandler.FindCustomer(PhoneNumber);
			if (customerDTO != null) {
				return Page();
			}

			if (_pizzaShopDbHandler.FindManager(PhoneNumber) == null &&
				_pizzaShopDbHandler.FindEmployee(PhoneNumber) == null) {
				return Content("This phone number isn't associated with any employee or manager account.");
			}

			HttpContext.Session.SetString("PhoneNumber", PhoneNumber);

			return RedirectToPage("/Store/Transactions");
		}
    }
}
    