
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public enum PizzaSize
    {
        Small,
        Medium,
        Large,
		[Display(Name = "Extra Large")]
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
		Pepperoni,
		Peppers,
    }

	public enum PizzaCrust
	{
		[Display(Name = "Original")]
		Original,
		[Display(Name = "Thin")]
		ThinCrust,
		[Display(Name = "Gluten Free")]
		GlutenFree,
		[Display(Name = "Stuffed")]
        StuffedCrust,
	}

    public enum PizzaExtra
	{
		[Display(Name = "3-Cheese Blend")]
		CheeseBlend,
		[Display(Name = "Extra Sause")]
		ExtraSause,
		[Display(Name = "Extra Cheese")]
        ExtraCheese,
    }

    public class Pizza
    {
        public PizzaSize Size { get; set; }
		public PizzaCrust Crust { get; set; }
        public IEnumerable<PizzaExtra> Extras { get; set; }
        public IEnumerable<PizzaTopping> Toppings { get; set; }

		public Pizza()
		{
			Extras = new List<PizzaExtra>();
			Toppings = new List<PizzaTopping>();
		}
    }
}