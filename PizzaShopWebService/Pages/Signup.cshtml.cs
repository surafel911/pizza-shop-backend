using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
    public class SignupModel : PageModel   
    {
		[Required]
		[BindProperty]
        public Customer Customer { get; set; }

		// TODO: Have this method use the DbHandler to create the account
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			return RedirectToPage("Order");
		}
	}
}
    