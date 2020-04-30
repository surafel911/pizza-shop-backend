using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Store
{
    public class IndexModel : PageModel
    {		
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;
		
		public IndexModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}
        public IActionResult OnGet()
        {
			IAccount account;
			string phoneNumber;

			ViewData["Store"] = true;
			
			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(phoneNumber)) {
				ViewData["Account"] = string.Empty;
				return Page();
			}

			account = _pizzaShopDbHandler.FindManager(phoneNumber);
			if (account == null) {
				account = _pizzaShopDbHandler.FindEmployee(phoneNumber);
				if (account == null) {
				}
			}

			if (account == null) {
				ViewData["Account"] = string.Empty;
			} else {
				ViewData["Account"] = account.Name;
			}

			return Page();
        }
    }
}