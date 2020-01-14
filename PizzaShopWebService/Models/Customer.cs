using System;
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
    }
}