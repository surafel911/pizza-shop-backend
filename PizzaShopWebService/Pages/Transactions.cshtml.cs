using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
    public class TransactionsModel : PageModel
    {
        
        public void OnGet()
        {
            Message = "all transaction";
        }
    }

}
