using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public enum DrinkSize
    {
        Small,
        Medium,
        Large,
        Liter,
    }

    public enum DrinkType
    {
		Coke,
		Pepsi,
		Sprite,
		[Display(Name = "Dr. Pepper")]
		DrPepper,
		[Display(Name = "Sweet Tea")]
		SweetTea,
    }

    public class Drink
    {
        public DrinkSize Size { get; set; }
        public DrinkType Type { get; set; }
    }
}