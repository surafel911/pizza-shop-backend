using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class IndexModel : PageModel
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		public bool LoggedIn { get; private set; }
		public ICollection<string> HomepagePizzaIconURLs { get; private set; }
		
	

		public IndexModel(IPizzaShopDbHandler pizzaShopDbHandler,
			IPizzaShopConfigDataLoader pizzaShopConfigDataLoader)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
			HomepagePizzaIconURLs = pizzaShopConfigDataLoader.SeedData.HomepagePizzaIconURLs;
		}

		public IActionResult OnGet()
		{
			string phoneNumber;
			CustomerDTO customerDTO;
			
			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			LoggedIn = !string.IsNullOrEmpty(phoneNumber);

			if (LoggedIn) {
				customerDTO = _pizzaShopDbHandler.FindCustomer(phoneNumber);
				if (customerDTO == null) {
					return Content("No customer account associated w/ saved phone number. Log out if manager or employee account.");
				}

			} else {
				ViewData["Account"] = string.Empty;
			}

			ViewData["Store"] = false;

			return Page();
		}
    }
}