using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class ReceiptModel : PageModel
    {
        private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

        [BindProperty]
        public TransactionDTO Transaction { get; set; }

        public ReceiptModel(IPizzaShopDbHandler pizzaShopDbHandler)
        {
            _pizzaShopDbHandler = pizzaShopDbHandler;
        }

        public IActionResult OnGet()
        {
            string phoneNumber;
            CustomerDTO customer;

            phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

            customer = _pizzaShopDbHandler.FindCustomer(phoneNumber);
            if (customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
            }

			return Page();
        }
    }
}
