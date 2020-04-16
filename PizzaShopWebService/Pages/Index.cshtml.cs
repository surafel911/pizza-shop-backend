using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class IndexModel : PageModel
    {
		public ICollection<string> HomepagePizzaIconURLs { get; private set; }
		
		public IndexModel(IPizzaShopConfigDataLoader pizzaShopConfigDataLoader)
		{
			HomepagePizzaIconURLs = pizzaShopConfigDataLoader.SeedData.HomepagePizzaIconURLs;
		}
    }
}