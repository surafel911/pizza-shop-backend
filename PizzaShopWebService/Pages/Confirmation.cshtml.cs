using System.Net;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
    public class ConfirmationModel : PageModel
    {
		[BindProperty]
		public bool CardUsed { get; set; }

		[BindProperty]
		public decimal Total { get; set; }

		[BindProperty]
		public Payment Payment { get; set; }

		[BindProperty]
		public PaymentType PaymentType { get; set; }

		// TODO: Confirmation is always redirected to w/ a PaymentType in the session.
		//		 Check to see if it's a card type, and if so, get the Payment class from
		//		 the session.

		// TODO: Order cannot be null here. There has to be an order to accesss this page.
        public IActionResult OnGet()
        {
			string data;

			data = HttpContext.Session.GetString("Order");
			if (string.IsNullOrEmpty(data)) {
				return Content("Order is required to access this page.");
			}
			
			Total = JsonSerializer.Deserialize<Order>(data).Total;

			PaymentType = (PaymentType)HttpContext.Session.GetInt32("PaymentType");

			if (PaymentType != PaymentType.Cash && 
				PaymentType != PaymentType.Check) {
				data = HttpContext.Session.GetString("Payment");
				Payment = JsonSerializer.Deserialize<Payment>(data);
				
				CardUsed = true;
			} else {
				Payment = null;

				CardUsed = false;
			}

			return Page();
        }

		public IActionResult OnPost()
		{
			string data;

			data = HttpContext.Session.GetString("PhoneNumber");

			


			HttpContext.Session.Clear();
			HttpContext.Session.SetString("PhoneNumber", data);

			return RedirectToPage("/Receipt");
		}
    }
}
