using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class Order
    {
        [DataType(DataType.Currency)]
        public double Total { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}