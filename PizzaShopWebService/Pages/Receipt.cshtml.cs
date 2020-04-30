using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class ReceiptModel : PageModel
    {
        private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		public bool CardUsed { get; private set; }

		public string LastFourOfCardNumber { get; private set; }

		public ItemsDTO ItemsDTO { get; private set; }

		public CustomerDTO CustomerDTO { get; private set; }

        public TransactionDTO TransactionDTO { get; private set; }

        public ReceiptModel(IPizzaShopDbHandler pizzaShopDbHandler)
        {
            _pizzaShopDbHandler = pizzaShopDbHandler;
        }

        public IActionResult OnGet()
        {
            string data;

            data = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(data)) {
				// TODO: Handle this condition better
				return Content("Login required.");
			}

            CustomerDTO = _pizzaShopDbHandler.FindCustomer(data);
            if (CustomerDTO == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
            }

			ViewData["Store"] = false;
			ViewData["Account"] = CustomerDTO.Name;

			data = HttpContext.Session.GetString("TransactionDTO");
			if (string.IsNullOrEmpty(data)) {
				// TODO: Handle this condition better
				return Content("Transaction required.");
			}

			TransactionDTO = JsonSerializer.Deserialize<TransactionDTO>(data);
			ItemsDTO = JsonSerializer.Deserialize<ItemsDTO>(TransactionDTO.ItemsJson);

			if (TransactionDTO.PaymentType != PaymentType.Cash && 
				TransactionDTO.PaymentType != PaymentType.Check) {
				CardUsed = true;
				data = JsonSerializer.Deserialize<PaymentDTO>(HttpContext.Session.GetString("PaymentDTO")).CardNumber;
				LastFourOfCardNumber = data.Substring(data.Length - 4);
			} else {
				CardUsed = false;
				LastFourOfCardNumber = string.Empty;
			}

			return Page();
        }

		public IActionResult OnGetReturnToIndexPage()
		{
			string phoneNumber = HttpContext.Session.GetString("PhoneNumber");

			HttpContext.Session.Clear();
			HttpContext.Session.SetString("PhoneNumber", phoneNumber);

			return RedirectToPagePermanent("/Index");
		}
    }
}
