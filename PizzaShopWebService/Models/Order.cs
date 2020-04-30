using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopWebService.Models
{
    public class Order
    {
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

		public ItemsDTO ItemsDTO { get; set; }

		public RetrievalType RetrievalType { get; set; }

		public Order()
		{
			ItemsDTO = new ItemsDTO();
		}

		public Order(ItemsDTO itemsDTO, RetrievalType retrievalType)
		{
			ItemsDTO = itemsDTO;
			RetrievalType = retrievalType;
		}
    }
}