using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class PriceManager
    {
		[DataType(DataType.Currency)]
		public static decimal DeliveryFee { get { return 4; } }

		[DataType(DataType.Currency)]
		public static decimal PizzaExtraPrice { get { return 1; } }

		[DataType(DataType.Currency)]
		public static decimal PizzaToppingPrice { get { return 0.75M; } }

		[DataType(DataType.Currency)]
		public static decimal PizzaCrustPrice { get { return 1; } }

		public static decimal CalculateOrderPrice(Order order)
		{
			decimal total = CalculateDeliveryPrice(order.RetrievalType);

			foreach (Pizza pizza in order.ItemsDTO.Pizzas) {
				total += CalculatePizzaPrice(pizza);
			}

			foreach (Drink drink in order.ItemsDTO.Drinks) {
				total += CalculateDrinkPrice(drink);
			}

			return total;
		}

		public static decimal CalculateItemsPrice(ItemsDTO itemsDTO)
		{
			decimal total = 0.0M;

			foreach (Pizza pizza in itemsDTO.Pizzas) {
				total += CalculatePizzaPrice(pizza);
			}

			foreach (Drink drink in itemsDTO.Drinks) {
				total += CalculateDrinkPrice(drink);
			}

			return total;
		}

		public static decimal CalculatePizzaPrice(Pizza pizza)
		{
			decimal Total = (6M + 3M * (int)pizza.Size);

			Total += (pizza.Crust == PizzaCrust.StuffedCrust ? PizzaCrustPrice : 0);

			foreach (PizzaExtra extra in pizza.Extras) {
				Total += PizzaExtraPrice;
			}

			foreach (PizzaTopping topping in pizza.Toppings) {
				Total += PizzaToppingPrice;
			}

			return Total;
		}

		public static decimal CalculateDrinkPrice(Drink drink)
		{
			return 1 + (int)drink.Size;
		}

		public static decimal CalculateDeliveryPrice(RetrievalType retrievalType)
		{
			return (decimal)(retrievalType == RetrievalType.Carryout ? 0M : DeliveryFee);
		}
    }
}