using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages
{
    public class IndexModel : PageModel
    {
		private readonly IPizzaShopSeedDataLoader _pizzaShopSeedDataLoader;

		public ICollection<string> PizzaIconURLs 
		{ 
			get 
			{
				return _pizzaShopSeedDataLoader.SeedData.PizzaIconURLs; 
			}

			private set
			{
			}
		}
		
		public IndexModel(IPizzaShopSeedDataLoader pizzaShopSeedDataLoader)
		{
			_pizzaShopSeedDataLoader = pizzaShopSeedDataLoader;
		}
    }
}
