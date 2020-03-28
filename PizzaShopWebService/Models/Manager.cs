using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
    public class Manager
    {

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password;
        
        [Required]
        public string Name { get; set; }
    }
}