using System.Net;
using System.Runtime.Serialization.Json;
using System;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class OrderModel : PageModel
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty]
		public Order Order { get; set; }

		[BindProperty]
		public Customer Customer { get; private set; }

		public OrderModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			Customer = new Customer();
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			string order, phoneNumber;

			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(phoneNumber)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

			Customer = _pizzaShopDbHandler.FindCustomer(phoneNumber);
			if (Customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			// TODO: Handle conditions for employee and manager accounts.

			order = HttpContext.Session.GetString("Order");
			Order = (!string.IsNullOrEmpty(order) ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			return Page();
        }


		// TODO: Order cannot be null here. There has to be an order to accesss this page.z
		public IActionResult OnPost()
		{
			string order;

			order = HttpContext.Session.GetString("Order");
			Order = (!string.IsNullOrEmpty(order) ? JsonSerializer.Deserialize<Order>(order) : 
				new Order());
			
			Order.CalculateTotalPrice();
			
			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));

			return RedirectToPage("/PaymentMethod");
		}
    }
}
    