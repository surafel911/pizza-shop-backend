using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
	// TODO: Maybe rename this to order
	public class MenuModel : PageModel    
	{
		[BindProperty]
		public Order Order { get; set; }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			// TODO: Learn how to pass this to next page
			string orderJson = JsonSerializer.Serialize(Order);

			return RedirectToPage("./Order");
		}
    }
}
        