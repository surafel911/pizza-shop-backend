using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Session;

using Npgsql;
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
				options.UseNpgsql(Configuration.GetConnectionString("PizzaShopDbConnection")));

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
			using (PizzaShopDbContext pizzaShopDbContext = app.ApplicationServices.CreateScope()
				.ServiceProvider.GetRequiredService<PizzaShopDbContext>()) {

				try {
					if (env.IsDevelopment()) {
						app.UseDeveloperExceptionPage();

						pizzaShopDbContext.Database.EnsureDeleted();
						pizzaShopDbContext.Database.EnsureCreated();

						pizzaShopDbContext.Add(new Customer
						{
							PhoneNumber = "1111111111",
							Password = "password",
							Name = "Surafel Assefa",
							Address = "White House",
							PaymentType = PaymentType.VisaCard,
						});
						pizzaShopDbContext.SaveChanges();
					}
					else
					{
						app.UseExceptionHandler("/Error");

						// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
						app.UseHsts();

						pizzaShopDbContext.Database.Migrate();
					}
				} catch (PostgresException e) {
					throw e;
				}
			}

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
