using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using PizzaShopWebService.Data;
using PizzaShopWebService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PizzaShopWebService.Services
{
    public class PizzaShopDbHandler : IPizzaShopDbHandler
    {
		private readonly IConfiguration _configuration;
		private readonly PizzaShopDbContext _pizzaShopDbContext;

		private void AddEntity<T>(T entity)
		{
			_pizzaShopDbContext.Add(entity);
			_pizzaShopDbContext.SaveChanges();

			File.AppendAllText(_configuration["FilePath"],
				JsonSerializer.Serialize(entity, new JsonSerializerOptions{
    				WriteIndented = true,
				}) + Environment.NewLine
			);		
		}

        public PizzaShopDbHandler(IConfiguration configuration, PizzaShopDbContext pizzaShopDbContext)
		{
			_configuration = configuration;
			_pizzaShopDbContext = pizzaShopDbContext;

			if (File.Exists(_configuration["FilePath"])) {
				File.Delete(_configuration["FilePath"]);
			}
		}

		public void AddManager(ManagerDTO managerDTO)
		{
			AddEntity(managerDTO);
		}

		public void AddEmployee(EmployeeDTO employeeDTO)
		{
			AddEntity(employeeDTO);
		}

		public void AddCustomer(CustomerDTO customerDTO)
		{
			AddEntity(customerDTO);
		}

		public void AddTransaction(TransactionDTO transactionDTO)
		{
			AddEntity(transactionDTO);
		}

		public void UpdateCustomer(CustomerDTO customerDTO)
		{
			_pizzaShopDbContext.Customers.Update(customerDTO);
			_pizzaShopDbContext.SaveChanges();
		}

		public ManagerDTO FindManager(string phoneNumber)
		{
			ManagerDTO managerDTO = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return managerDTO;
			}

			managerDTO = _pizzaShopDbContext.Managers.Find(phoneNumber);

			return managerDTO;
		}

		public EmployeeDTO FindEmployee(string phoneNumber)
		{
			EmployeeDTO employeeDTO = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return employeeDTO;
			}

			employeeDTO = _pizzaShopDbContext.Employees.Find(phoneNumber);

			return employeeDTO;
		}

		public CustomerDTO FindCustomer(string phoneNumber)
		{
			CustomerDTO customer = null;

			if (string.IsNullOrEmpty(phoneNumber)) {
				return customer;
			}
			
			customer = _pizzaShopDbContext.Customers.Find(phoneNumber);

			return customer;
		}

		public Task<List<TransactionDTO>> GetTransactions()
		{
			return _pizzaShopDbContext.Transactions.ToListAsync();
		}
    }
}