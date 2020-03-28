using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using PizzaShopWebService.Models;

namespace PizzaShopWebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
                How to enumerate an enum
                foreach (string v in Enum.GetNames(typeof(PizzaSize))) {
                    Console.WriteLine(v);
                }
            */

             CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}