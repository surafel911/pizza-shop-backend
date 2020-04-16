using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
	public interface IAccount
	{
		[Required]
		[Phone]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Password)]
		string Password { get; set; }

		[Required]
		string Name { get; set; }
	}
}