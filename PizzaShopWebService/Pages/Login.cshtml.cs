using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PizzaShopWebService.Pages
{
    public class LoginModel : PageModel   
    {	
        [Phone]
		[Required]
		[BindProperty]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Required]
		[BindProperty]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) {
     		   return Page();
    		}

			return RedirectToPage("/Order");
		}
    }
}
    