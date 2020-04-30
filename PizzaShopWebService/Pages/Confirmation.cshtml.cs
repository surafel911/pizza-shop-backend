using System.Net;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services	;

namespace PizzaShopWebService.Pages
{
    public class ConfirmationModel : PageModel
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		public bool CardUsed { get; private set; }

		public decimal Total { get; private set; }

		public string LastFourOfCardNumber { get; private set; }

		public PaymentDTO PaymentDTO { get; private set; }

		public PaymentType PaymentType { get; private set; }


		// TODO: Confirmation is always redirected to w/ a PaymentType in the session.
		//		 Check to see if it's a card type, and if so, get the Payment class from
		//		 the session.

        public ConfirmationModel(IPizzaShopDbHandler pizzaShopDbHandler)
        {
            _pizzaShopDbHandler = pizzaShopDbHandler;
        }


		// TODO: Order cannot be null here. There has to be an order to accesss this page.
        public IActionResult OnGet()
        {
			string data;
			CustomerDTO customer;

			data = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(data)) {
				// TODO: Handle this condition better
                return Content("Login required.");
			}

			customer = _pizzaShopDbHandler.FindCustomer(data);
			if (customer == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			ViewData["Store"] = false;
			ViewData["Account"] = customer.Name;

			data = HttpContext.Session.GetString("Order");
			if (string.IsNullOrEmpty(data)) {
				return Content("Order is required to access this page.");
			}
			
			Total = JsonSerializer.Deserialize<Order>(data).Total;

			PaymentType = (PaymentType)HttpContext.Session.GetInt32("PaymentType");

			if (PaymentType != PaymentType.Cash && 
				PaymentType != PaymentType.Check) {
				CardUsed = true;
				data = HttpContext.Session.GetString("PaymentDTO");
				PaymentDTO = JsonSerializer.Deserialize<PaymentDTO>(data);
				LastFourOfCardNumber = PaymentDTO.CardNumber.Substring(PaymentDTO.CardNumber.Length - 4);
			} else {
				CardUsed = false;
				PaymentDTO = null;
				LastFourOfCardNumber = string.Empty;
			}

			return Page();
        }

		public IActionResult OnPost()
		{
			string order, payment, phoneNumber;

			order = HttpContext.Session.GetString("Order");
			payment = HttpContext.Session.GetString("PaymentDTO");
			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			
			TransactionDTO transactionDTO = new TransactionDTO(
				JsonSerializer.Deserialize<Order>(order),
				_pizzaShopDbHandler.FindCustomer(phoneNumber),
				(PaymentType)HttpContext.Session.GetInt32("PaymentType")
			);

			_pizzaShopDbHandler.AddTransaction(transactionDTO);

			HttpContext.Session.Clear();
			HttpContext.Session.SetString("PhoneNumber", phoneNumber);
			HttpContext.Session.SetString("TransactionDTO", JsonSerializer.Serialize(transactionDTO));
			
			if (!string.IsNullOrEmpty(payment)) {
				HttpContext.Session.SetString("PaymentDTO", payment);
			}

			return RedirectPermanent("/Receipt");
		}
    }
}
