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
		public CustomerDTO CustomerDTO { get; private set; }

		public OrderModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
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

			CustomerDTO = _pizzaShopDbHandler.FindCustomer(phoneNumber);
			if (CustomerDTO == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			ViewData["Store"] = false;
			ViewData["Account"] = CustomerDTO.Name;

			// TODO: Handle conditions for employee and manager accounts.

			order = HttpContext.Session.GetString("Order");
			Order = (!string.IsNullOrEmpty(order) ? JsonSerializer.Deserialize<Order>(order) :
				new Order());

			return Page();
        }


		// TODO: Order cannot be null here. There has to be an order to accesss this page.z
		public IActionResult OnPost()
		{
			RetrievalType retrievalType;

			retrievalType = Order.RetrievalType;

			Order = JsonSerializer.Deserialize<Order>(HttpContext.Session.GetString("Order"));
			Order.RetrievalType = retrievalType;
			Order.Total = PriceManager.CalculateOrderPrice(Order);
			
			HttpContext.Session.SetString("Order", JsonSerializer.Serialize(Order));

			return RedirectToPage("/PaymentMethod");
		}
    }
}
    