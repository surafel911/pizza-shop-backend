using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
	// TODO: Figure out why this validation is failing
    public class Payment
    {
		[Required]
		[DataType(DataType.CreditCard)]
		public string CardNumber { get; set; }

		[Required]
		public string CVC { get; set; }
		
		[Required]
		public string Name { get; set; }

		[Required]
		public PaymentType PaymentType { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Expiraton Date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:Y}")]
		public DateTime ExpirationDate { get; set; }
    }
}