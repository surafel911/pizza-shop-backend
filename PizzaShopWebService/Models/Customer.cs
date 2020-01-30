
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
    public class Customer
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        public string AddressDetails { get; set; }

        // Navagation properties
        public ICollection<Transaction> Transactions { get; set; }
    }
}