using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
    public class Transaction
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerPhoneNumber { get; set; }
        
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; }
    }
}