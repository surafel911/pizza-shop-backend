using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Customer
{
    public class SignupModel : PageModel   
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty]
        public CustomerDTO CustomerDTO { get; set; }

		public string ErrorMessage { get; private set; }

		public SignupModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			ViewData["Store"] = false;

			return Page();
		}

		public IActionResult OnPost()
		{
			ViewData["Store"] = false;
			
			if (!ModelState.IsValid) {
				return Page();
			}

			if (_pizzaShopDbHandler.FindManager(CustomerDTO.PhoneNumber) != null ||
				_pizzaShopDbHandler.FindEmployee(CustomerDTO.PhoneNumber) != null ||
				_pizzaShopDbHandler.FindCustomer(CustomerDTO.PhoneNumber) != null) {
				ErrorMessage = "This phone number is registered with another account. Please try a different phone number.";
				return Page();
			}

			_pizzaShopDbHandler.AddCustomer(CustomerDTO);

			return RedirectToPage("/Customer/Login");
		}
	}
}