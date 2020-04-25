using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
	public class MenuModel : PageModel    
	{
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty]
		public Order Order { get; set; }
	
		[BindProperty]
		public Drink CurrentDrink { get; set; }

		[BindProperty]
		public Pizza CurrentPizza { get; set; }
		
		// TODO: Is a new Model created every time a request is made? Break here and see if constructor is called after post
		public MenuModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			CurrentDrink = new Drink();
			CurrentPizza = new Pizza();

			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			string data;
			CustomerDTO customer;

			data = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(data)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

			// TODO: Handle conditions for employee and manager accounts.

			customer = _pizzaShopDbHandler.FindCustomer(data);
			if (customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			order = HttpContext.Session.GetString("Order");
			Order = (!string.IsNullOrEmpty(order) ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			return Page();
		}

		public IActionResult OnPost()
		{
			string order;

			if (!ModelState.IsValid) {
				return Page();
			}

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			if (Order.Items.Drinks.Count == 0 && Order.Items.Pizzas.Count == 0) {
				return Page();
			}

			return RedirectToPage("/Order");
		}

		public IActionResult OnPostAddPizza()
		{
			string order;

			if (!ModelState.IsValid) {
				return Page();
			}

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			Order.Items.Pizzas.Add(CurrentPizza);
			Order.CalculateTotalPrice();

			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));

			return RedirectToPage("/Menu");
		}

		public IActionResult OnPostAddDrink()
		{
			string order;
			
			if (!ModelState.IsValid) {
				return Page();
			}

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			Order.Items.Drinks.Add(CurrentDrink);
			Order.CalculateTotalPrice();

			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));

			return RedirectToPage("/Menu");
		}

		public IActionResult OnPostRestartOrder()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			HttpContext.Session.Remove("Order");

			return RedirectToPage("/Menu");
		}
    }
}
        