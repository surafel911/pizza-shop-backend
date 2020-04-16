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

		[BindProperty]
		public RetrievalType RetrievalType { get; set; }

		public string PhoneNumber { get; set; }
		
		// TODO: Is a new Model created every time a request is made? Break here and see if constructor is called after post
		public MenuModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			CurrentDrink = new Drink();
			CurrentPizza = new Pizza();

			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			string order;
			int? retrievalType;

			PhoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(PhoneNumber)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

			Customer customer = _pizzaShopDbHandler.FindCustomer(PhoneNumber);
			if (customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			// TODO: Handle conditions for employee and manager accounts.

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) : new Order());

			retrievalType = HttpContext.Session.GetInt32("RetrievalType");
			RetrievalType = (retrievalType != null ? (RetrievalType)retrievalType.Value : new RetrievalType());

			return Page();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			Order.CalculateTotalPrice();

			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));
			HttpContext.Session.SetInt32("RetrievalType", (int)RetrievalType);

			return RedirectToPage("/Order");

		}

		public IActionResult OnPostAddPizza()
		{
			string order;

			if (!ModelState.IsValid) {
				return Page();
			}

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) : new Order());

			Order.Pizzas.Add(CurrentPizza);
			Order.CalculateTotalPrice();

			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));
			HttpContext.Session.SetInt32("RetrievalType", (int)RetrievalType);

			return RedirectToPage("/Menu");
		}

		public IActionResult OnPostAddDrink()
		{
			string order;
			
			if (!ModelState.IsValid) {
				return Page();
			}

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) : new Order());

			Order.Drinks.Add(CurrentDrink);
			Order.CalculateTotalPrice();

			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));
			HttpContext.Session.SetInt32("RetrievalType", (int)RetrievalType);

			return RedirectToPage("/Menu");
		}

		public IActionResult OnPostRestartOrder()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			HttpContext.Session.Remove("Order");
			HttpContext.Session.Remove("RetrievalType");

			return RedirectToPage("/Menu");
		}
    }
}
        