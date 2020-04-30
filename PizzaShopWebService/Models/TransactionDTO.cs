using System;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopWebService.Models
{
	public class TransactionDTO
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
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
		public DateTime Date { get; set; }

		[Required]
		public PaymentType PaymentType { get; set; }

		[Required]
		[DataType(DataType.Currency)]

		public decimal Total { get; set; }

		[Required]
		public RetrievalType RetrievalType { get; set; }

		[Required]
		public string ItemsJson { get; set; }

		// Navagation properties
		public CustomerDTO CustomerDTO;

		public TransactionDTO()
		{
		}

		public TransactionDTO(Order order, CustomerDTO customerDTO, PaymentType paymentType)
		{
			CustomerPhoneNumber = customerDTO.PhoneNumber;
			Date = DateTime.Now;
			PaymentType = paymentType;
			Total = order.Total;
			RetrievalType = order.RetrievalType;
			ItemsJson = JsonSerializer.Serialize(order.ItemsDTO);
			CustomerDTO = customerDTO;
		}
	}
}