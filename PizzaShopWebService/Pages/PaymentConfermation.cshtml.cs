﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaShopWebService.Pages
{
    public class PaymentConfermationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "customer";
        }
    }
}
