using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class Order
    {
        [DataType(DataType.Currency)]
        public double Total { get; private set; }

        public ICollection<Pizza> Pizzas
		{
			get
			{
				return Pizzas;
			}
			
			set
			{
				Pizzas = value;
				CalculatePrice();
			}
		}

        public ICollection<Drink> Drinks
		{
			get
			{
				return Drinks;
			}
			
			set
			{
				Drinks = value;
				CalculatePrice();
			}
		}

		public void CalculatePrice()
		{
			Total = 0.0;

			foreach (Pizza pizza in Pizzas) {
				Total += 6.0 + 3 * (int)pizza.Size;

				foreach (PizzaExtras extra in pizza.Extras) {
					Total += 0.75;
				}

				foreach (PizzaTopping topping in pizza.Toppings) {
					Total += 0.75;
				}
			}

			foreach (Drink drink in Drinks) {
				Total += 1.5;
			}
		}
    }
}