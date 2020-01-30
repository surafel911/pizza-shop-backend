namespace PizzaShopWebService.Models
{
    public enum DrinkSize
    {
        Small,
        Medium,
        Large,
        Liter,
        TwoLiter,
    }

    public enum DrinkType
    {
        Coke,
        Pepsi,
        DoctorPepper,
    }

    public class Drink
    {
        public DrinkSize Size { get; set; }
        public DrinkType Type { get; set; }
    }
}