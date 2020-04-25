using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
	public class ManagerDTO : IAccount
	{

		[Required]
		[Phone]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[Required]
		public string Name { get; set; }
	}
}