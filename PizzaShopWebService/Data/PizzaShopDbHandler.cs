using PizzaShopWebService.Models;

namespace PizzaShopWebService.Data
{
	// TODO: Make this implementation more specific to the data model
    public class PizzaShopDbHandler
    {
		private readonly PizzaShopDbContext _pizzaShopDbContext;

        public PizzaShopDbHandler(PizzaShopDbContext pizzaShopDbContext)
		{
			_pizzaShopDbContext = pizzaShopDbContext;
		}
    }
}