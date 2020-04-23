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
			string order, PhoneNumber;

			PhoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(PhoneNumber)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

			Customer = _pizzaShopDbHandler.FindCustomer(PhoneNumber);
			if (Customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			// TODO: Handle conditions for employee and manager accounts.

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) : new Order());

			return Page();
        }

		public IActionResult OnPost()
		{
			string order;
			Transaction transaction;
			RetrievalType retrievalType;

			retrievalType = Order.RetrievalType;

			order = HttpContext.Session.GetString("Order");
			Order = (order != null ? JsonSerializer.Deserialize<Order>(order) : new Order());
			
			Order.RetrievalType = retrievalType;
			Order.CalculateTotalPrice();

			transaction = new Transaction();

			transaction.Date = DateTime.Now;
			transaction.Total = Order.Total;
			transaction.Customer = Customer;
			transaction.PaymentType = Customer.PaymentType;
			transaction.RetrievalType = RetrievalType.Carryout;
			transaction.CustomerPhoneNumber = Customer.PhoneNumber;
			transaction.OrderJson = JsonSerializer.Serialize(Order);

			_pizzaShopDbHandler.AddTransaction(transaction);

			return RedirectToPage("/Index");
		}
    }
}
    