using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public interface IPizzaShopSeedDataLoader
    {
        PizzaShopSeedData SeedData { get; }
    }
}