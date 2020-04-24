using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaShopWebService.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "customer";
        }

		// TODO: Confirmation needs to read whether "PaymentType.Cash" is in the session.
		//		 If so, then proceed without card detais. If not, check for card details
		//		 in session.
    }
}
