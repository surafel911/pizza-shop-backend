using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PizzaShopWebService.Models
{
    public enum PizzaSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    public enum PizzaTopping
    {
        Beef,
        Bacon,
        Olives,
        Sausage,
        Spinach,
        Mushrooms,
    }

    public enum PizzaExtras
    {
        ExtraCheese,
        ExtraSause,
        StuffedCrust,
    }

    public class Pizza
    {
        public PizzaSize Size { get; set; }
        public ICollection<PizzaExtras> Extras { get; set; }
        public ICollection<PizzaTopping> Toppings { get; set; }
    }
}