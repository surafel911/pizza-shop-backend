using System.Collections.Generic;

namespace PizzaShopWebService.Models
{
    public class Items
    {
        public ICollection<Drink> Drinks { get; set ;}
        public ICollection<Pizza> Pizzas { get; set; }

        public Items()
        {
            Drinks = new List<Drink>();
            Pizzas = new List<Pizza>();
        }
    }
}