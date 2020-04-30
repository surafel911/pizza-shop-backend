﻿using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService.Pages.Customer
{
    public class LoginModel : PageModel   
    {
		private readonly IPizzaShopDbHandler _pizzaShopDbHandler;

		[Required]
		[BindProperty]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Required]
		[BindProperty]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public LoginModel(IPizzaShopDbHandler pizzaShopDbHandler)
		{
			_pizzaShopDbHandler = pizzaShopDbHandler;
		}

		public IActionResult OnGet()
		{
			ViewData["Store"] = false;

			return Page();
		}

		public IActionResult OnPost()
		{
			ViewData["Store"] = false;

			if (!ModelState.IsValid) {
     			return Page();
    		}

			CustomerDTO customerDTO = _pizzaShopDbHandler.FindCustomer(PhoneNumber);
			if (customerDTO == null) {
				return Content("This phone number isn't registered to an account. Please sign up.");
			}

			EmployeeDTO employeeDTO = _pizzaShopDbHandler.FindEmployee(PhoneNumber);
			if (employeeDTO != null) {
				return Content("This phone number is registered with an employee account. Please use a differnet phone number.");
			}

			ManagerDTO managerDTO = _pizzaShopDbHandler.FindManager(PhoneNumber);
			if (managerDTO != null) {
				return Content("This phone number is registered with a manager account. Please use a differnet phone number.");
			}

			HttpContext.Session.SetString("PhoneNumber", PhoneNumber);

			return RedirectToPage("/Index");
		}
    }
}
    