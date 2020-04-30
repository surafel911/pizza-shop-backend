using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Customer
{
    public class ManageModel : PageModel   
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty]
        public CustomerDTO CustomerDTO { get; set; }

		public string ErrorMessage { get; private set; }

		public ManageModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			ViewData["Store"] = false;

			string phoneNumber = HttpContext.Session.GetString("PhoneNumber");

			if (string.IsNullOrEmpty(phoneNumber)) {
				// TODO: Handle this condition better
                return Content("Login required.");
			}

			CustomerDTO = _pizzaShopDbHandler.FindCustomer(phoneNumber);
			if (CustomerDTO == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			ViewData["Account"] = CustomerDTO.Name;

			return Page();
		}

		public IActionResult OnPost()
		{
			string phoneNumber = HttpContext.Session.GetString("PhoneNumber");

			ViewData["Store"] = false;
			ViewData["Account"] = _pizzaShopDbHandler.FindCustomer(phoneNumber).Name;

			if (!ModelState.IsValid) {
				return Page();
			}
			
			CustomerDTO.PhoneNumber = phoneNumber;
			_pizzaShopDbHandler.UpdateCustomer(CustomerDTO);

			return RedirectToPagePermanent("/Index");
		}
	}
}