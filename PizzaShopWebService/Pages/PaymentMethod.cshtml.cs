using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class PaymentMethodModel : PageModel
    {
        private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

        [BindProperty]
        public PaymentType PaymentType { get; set; }

        public PaymentMethodModel(IPizzaShopDbHandler pizzaShopDbHandler)
        {
            _pizzaShopDbHandler = pizzaShopDbHandler;
        }

        public IActionResult OnGet()
        {
            string phoneNumber;
            Customer customer;

            phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber)) {
                // TODO: Handle this condition better
                return Content("Login required.");
            }

            customer = _pizzaShopDbHandler.FindCustomer(phoneNumber);
            if (customer == null) {
                // TODO: Handle this condition better
                return Content("No customer account in this phone number.");
            }

            PaymentType = customer.PaymentType;

            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetInt32("PaymentType", (int)PaymentType);

            if (PaymentType == PaymentType.Cash || 
                PaymentType == PaymentType.Check) {
                return RedirectToPage("/Confirmation");
            }
            
            return RedirectToPage("/Payment");
        }
    }
}
