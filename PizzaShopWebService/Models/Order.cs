using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class Order
    {
		private static readonly decimal deliveryFee = 4;

		public decimal DeliveryFee { get { return deliveryFee; } }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
		
		public RetrievalType RetrievalType { get; set; }

		public ICollection<Drink> Drinks { get; set; }

		public ICollection<Pizza> Pizzas { get; set; }

		public Order()
		{
			Drinks = new List<Drink>();
			Pizzas = new List<Pizza>();
		}

		public void CalculateTotalPrice()
		{
			Total = CalculateDeliveryPrice();

			foreach (Pizza pizza in Pizzas) {
				Total += CalculatePizzaPrice(pizza);
			}

			foreach (Drink drink in Drinks) {
				Total += CalculateDrinkPrice(drink);
			}
		}

		public decimal CalculatePizzaPrice(Pizza pizza)
		{
			decimal Total = (decimal)(6.0 + 3 * (int)pizza.Size);

			Total += (decimal)(pizza.Crust != PizzaCrust.Original ? 1 : 0);

			foreach (PizzaExtra extra in pizza.Extras) {
				Total += (decimal)0.75;
			}

			foreach (PizzaTopping topping in pizza.Toppings) {
				Total += (decimal)0.75;
			}

			return Total;
		}

		public decimal CalculateDrinkPrice(Drink drink)
		{
			return 1 + (int)drink.Size;
		}

		public decimal CalculateDeliveryPrice()
		{
			return (decimal)(RetrievalType == RetrievalType.Carryout ? 0 : DeliveryFee);
		}
    }
}