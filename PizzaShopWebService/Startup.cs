using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Data;
using PizzaShopWebService.Models;
using PizzaShopWebService.Services;

namespace PizzaShopWebService
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			services.AddDbContext<PizzaShopDbContext>(options => 
			{
				options.UseInMemoryDatabase("pizzashopdb");
			});

			services.AddScoped<IPizzaShopDbHandler, PizzaShopDbHandler>();

			services.AddSingleton<IPizzaShopConfigDataLoader, PizzaShopConfigDataLoader>();

			services.AddSession(options => {
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.IdleTimeout = TimeSpan.FromMinutes(5);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			Console.WriteLine("info: Database file saved to {0}. Access at the end of execution.", 
				System.IO.Directory.GetCurrentDirectory() + 
				(new System.IO.FileInfo(Configuration["FilePath"]).FullName));


			using (PizzaShopDbContext pizzaShopDbContext = app.ApplicationServices.CreateScope()
				.ServiceProvider.GetRequiredService<PizzaShopDbContext>()) {

				if (env.IsDevelopment()) {
					app.UseDeveloperExceptionPage();

					pizzaShopDbContext.Database.EnsureDeleted();
					pizzaShopDbContext.Database.EnsureCreated();
				}
				else
				{
					app.UseExceptionHandler("/Error");

					// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
					app.UseHsts();

					pizzaShopDbContext.Database.Migrate();
				}
			}

			IPizzaShopDbHandler pizzaShopDbHandler = app.ApplicationServices.CreateScope()
					.ServiceProvider.GetRequiredService<IPizzaShopDbHandler>();

				pizzaShopDbHandler.AddCustomer(new Customer
					{
						PhoneNumber = "1111111111",
						Password = "password",
						Name = "Mona ",
						Address = "1600 Pennsylvania Ave NW, Washington, DC 20500",
						PaymentType = PaymentType.VisaCard,
					});

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}