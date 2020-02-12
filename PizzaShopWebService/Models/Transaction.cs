using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
    public enum RetrievalType
    {
        Carryout,
        Delivery,
    }

    public class Transaction
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerPhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        [DataType(DataType.Currency)]

        public double Total { get; set; }

        [Required]
        public RetrievalType RetrievalType { get; set; }

        [Required]
        public string OrderJson { get; set; }

        // Navagation properties
        public Customer Customer;
    }
}