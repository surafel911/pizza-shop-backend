using System.Text.Json;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

using System;

namespace PizzaShopWebService.Pages
{
    public class PaymentModel : PageModel
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[Required]
		[BindProperty]
		public Payment Payment { get; set; }

		[BindProperty]
		public Customer Customer { get; set; }

		public PaymentModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

        public IActionResult OnGet()
        {
			string payment, phoneNumber;

			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(phoneNumber)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

			// TODO: Handle conditions for employee and manager accounts.

			Customer = _pizzaShopDbHandler.FindCustomer(phoneNumber);
			if (Customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			payment = HttpContext.Session.GetString("Payment");
			Payment = (payment != null ? JsonSerializer.Deserialize<Payment>(payment) : new Payment());

			Payment.Name = Customer.Name;
			Payment.PaymentType = Customer.PaymentType;

			return Page();
        }

		public IActionResult OnPost()
		{
			// TODO: Find out why validation is not working properly.
			
			if (!ModelState.IsValid) {
				Console.WriteLine("CardNumber: {0}", ModelState.GetValidationState("CardNumber").ToString());
				Console.WriteLine("Name: {0}", ModelState.GetValidationState("Name").ToString());
				Console.WriteLine("PaymentType: {0}", ModelState.GetValidationState("PaymentType").ToString());
				Console.WriteLine("ExpirationDate: {0}", ModelState.GetValidationState("ExpirationDate").ToString());
				Console.WriteLine("CVC: {0}", ModelState.GetValidationState("CVC").ToString());

				return Page();
			}

			HttpContext.Session.SetString("Payment", JsonSerializer.Serialize(Payment));

			return RedirectToPage("/Confirmation");
		}
    }
}
