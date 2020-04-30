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
		public PaymentDTO PaymentDTO { get; set; }

		[BindProperty]
		public CustomerDTO CustomerDTO { get; set; }

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

			CustomerDTO = _pizzaShopDbHandler.FindCustomer(phoneNumber);
			if (CustomerDTO == null) {
				// TODO: Handle this condition better
				return Content("No customer account in this phone number.");
			}

			ViewData["Store"] = false;
			ViewData["Account"] = CustomerDTO.Name;

			payment = HttpContext.Session.GetString("Payment");
			PaymentDTO = (payment != null ? JsonSerializer.Deserialize<PaymentDTO>(payment) :
				new PaymentDTO());

			PaymentDTO.Name = CustomerDTO.Name;
			PaymentDTO.PaymentType = CustomerDTO.PaymentType;

			return Page();
        }

		public IActionResult OnPost()
		{
			// TODO: Find out why validation is not working properly.\
			// TODO: Add manual validation
			
			// if (!ModelState.IsValid) {
			// 	return Page();
			// }

			HttpContext.Session.SetString("PaymentDTO", JsonSerializer.Serialize(PaymentDTO));

			return RedirectToPage("/Confirmation");
		}
    }
}
