using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopConfigDataLoader
    {
        PizzaShopConfigData SeedData { get; }
    }
}