using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
    public class CustomerDTO : IAccount
    {
		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		[Display(Name = "Prefered Payment Method")]
		public PaymentType PaymentType { get; set; }

		[Display(Name = "Address Details")]
		public string AddressDetails { get; set; }

		// Navagation properties
		public ICollection<TransactionDTO> Transactions { get; set; }
    }
}