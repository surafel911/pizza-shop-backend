using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
    public class ReceiptModel : PageModel
    {
		public Transaction Transaction { get; set; }

        public void OnGet()
        {
			
        }
    }
}
