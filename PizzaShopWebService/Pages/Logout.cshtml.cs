using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace PizzaShopWebService.Pages
{
    public class LogoutModel : PageModel
    {
		public IActionResult OnGet()
		{
			HttpContext.Session.Clear();

			ViewData["Store"] = false;
			ViewData["Account"] = string.Empty;

			return Page();
		}
    }
}