using System.Collections.Generic;

namespace PizzaShopWebService.Models
{
    public class ItemsDTO
    {
        public ICollection<Drink> Drinks { get; set ;}
        public ICollection<Pizza> Pizzas { get; set; }

        public ItemsDTO()
        {
            Drinks = new List<Drink>();
            Pizzas = new List<Pizza>();
        }
    }
}