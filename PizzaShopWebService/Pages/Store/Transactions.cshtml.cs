using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Store
{
    public class TransactionsModel : PageModel
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[BindProperty(SupportsGet = true)]
		[Phone]
		public string PhoneNumber { get; set; }

		public ICollection<TransactionDTO> TransactionDTOs { get; set; }
		
		public TransactionsModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
			TransactionDTOs = new List<TransactionDTO>();
		}


        public async Task<IActionResult> OnGetAsync()
        {
			IAccount account;
			string phoneNumber;
			
			
			phoneNumber = HttpContext.Session.GetString("PhoneNumber");
			if (string.IsNullOrEmpty(phoneNumber)) {
				// TODO: Handle this condition better
                return Content("Login required.");
			}

			account = _pizzaShopDbHandler.FindManager(phoneNumber);
			if (account == null) {
				account = _pizzaShopDbHandler.FindEmployee(phoneNumber);
				if (account == null) {
					return Content("This phone number isn't associated with any employee or manager account.");
				}
			}

			ViewData["Store"] = false;
			ViewData["Account"] = account.Name;

			TransactionDTOs = await _pizzaShopDbHandler.GetTransactions();

			if (!string.IsNullOrEmpty(PhoneNumber)) {
				TransactionDTOs = TransactionDTOs.Where(transactionDTO => transactionDTO.CustomerPhoneNumber == PhoneNumber).ToList();
			}

			return Page();
        }
    }
}
