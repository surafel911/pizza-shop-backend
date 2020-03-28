using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopWebService.Models
{
    public class Menu
    {
        public string menueId { get; set; }
        public string menuname{ get; set; }
        public string ImageReferaceId { get; set; }
        public double price { get; set; }
      
       
        public static List<Menu> ReturnAll()
        {
            var menus = new List<Menu>()
            {
               new Menu()
               {
                   menueId = "r4343534t43t432",
                   menuname = "Beef",
                   price = 12.99,
                    ImageReferaceId = "https://firebasestorage.googleapis.com/v0/b/testhouse-ff733.appspot.com/o/450apc84ihm?alt=media"
                    
      },
               new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Bacon",
                     price = 10.99,
                   ImageReferaceId = "https://firebasestorage.googleapis.com/v0/b/testhouse-ff733.appspot.com/o/ns9ob41l2y9?alt=media"
               },
              
                 new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Olives",
                     price = 14.99,
                   ImageReferaceId = "https://cdn.shopify.com/s/files/1/0717/6497/products/kalamata-olive-pizza-894409_2000x.jpg?v=1562089266"
               },
                  new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Sausage",
                     price = 9.99,
                   ImageReferaceId = "https://craftcms-pizzaranch.s3.amazonaws.com/general-uploads/Menu-Images/_960x800_crop_center-center_none/Sausage.png?mtime=20170517171226"
               },
                   new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Spinach",
                   price = 11.99,
                   ImageReferaceId = "https://upload.wikimedia.org/wikipedia/commons/a/a3/Eq_it-na_pizza-margherita_sep2005_sml.jpg"
               },
                    new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Mushrooms",
                     price = 10.99,
                   ImageReferaceId = "https://firebasestorage.googleapis.com/v0/b/testhouse-ff733.appspot.com/o/8i8raljtvm?alt=media"
               },
                     new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Chicken",
                     price = 15.99,
                   ImageReferaceId = "https://firebasestorage.googleapis.com/v0/b/testhouse-ff733.appspot.com/o/6zs7l1fvgqb?alt=media"
               },
                        new Menu()
               {
                    menueId = "r4343534t43t432",
                   menuname = "Cheese Pizza",
                     price = 9.99,
                   ImageReferaceId = "https://firebasestorage.googleapis.com/v0/b/testhouse-ff733.appspot.com/o/bwxlsff4mg?alt=media"
               }
            };
            return menus;
        }

     
    }
}
