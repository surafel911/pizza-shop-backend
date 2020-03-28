using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Pages
{
    public class SignupModel : PageModel   
    {
        public Customer Customer { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
    