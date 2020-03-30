using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public class PizzaShopSeedDataLoader : IPizzaShopSeedDataLoader
    {
		public PizzaShopSeedData SeedData { get; private set; }

		private void LoadSeedData(string seedDataPath)
		{
			StreamReader streamReader = new StreamReader(seedDataPath);
			SeedData = JsonSerializer.Deserialize<PizzaShopSeedData>(streamReader.ReadToEnd());
		}
		public PizzaShopSeedDataLoader(IConfiguration configuration)
		{
			LoadSeedData(configuration["SeedDataPath"]);
		}
    }
}