using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class Order
    {
		[DataType(DataType.Currency)]
		public decimal DeliveryFee { get { return 4; } }

		[DataType(DataType.Currency)]
		public decimal PizzaExtraPrice { get { return 1; } }

		[DataType(DataType.Currency)]
		public decimal PizzaToppingPrice { get { return 0.75M; } }

		[DataType(DataType.Currency)]
		public decimal PizzaCrustPrice { get { return 1; } }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
		
		public RetrievalType RetrievalType { get; set; }

		public Items Items { get; set; }

		public Order()
		{
			Items = new Items();
		}

		public void CalculateTotalPrice()
		{
			Total = CalculateDeliveryPrice();

			foreach (Pizza pizza in Items.Pizzas) {
				Total += CalculatePizzaPrice(pizza);
			}

			foreach (Drink drink in Items.Drinks) {
				Total += CalculateDrinkPrice(drink);
			}
		}

		public decimal CalculatePizzaPrice(Pizza pizza)
		{
			decimal Total = (6M + 3 * (int)pizza.Size);

			Total += (pizza.Crust == PizzaCrust.StuffedCrust ? PizzaCrustPrice : 0);

			foreach (PizzaExtra extra in pizza.Extras) {
				Total += PizzaExtraPrice;
			}

			foreach (PizzaTopping topping in pizza.Toppings) {
				Total += PizzaToppingPrice;
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