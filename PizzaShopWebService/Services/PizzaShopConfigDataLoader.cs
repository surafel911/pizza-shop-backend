using System;
using System.IO;
using System.Text.Json;

using Microsoft.Extensions.Configuration;

using PizzaShopWebService.Models;

namespace PizzaShopWebService.Services
{
    public class PizzaShopConfigDataLoader : IPizzaShopConfigDataLoader
    {
		public PizzaShopConfigData SeedData { get; private set; }

		private void LoadSeedData(string seedDataPath)
		{
			StreamReader streamReader = new StreamReader(seedDataPath);
			SeedData = JsonSerializer.Deserialize<PizzaShopConfigData>(streamReader.ReadToEnd());
		}
		public PizzaShopConfigDataLoader(IConfiguration configuration)
		{
			LoadSeedData(configuration["ConfigDataPath"]);
		}
    }
}