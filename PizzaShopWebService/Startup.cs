using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Data;
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

			services.AddSingleton<IPizzaShopSeedDataLoader>(
				new PizzaShopSeedDataLoader(Configuration)
			);

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<PizzaShopDbContext>();

			// TODO: Configure this to match project requirements
			services.Configure<IdentityOptions>(options =>
				{
					// Password settings.
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireNonAlphanumeric = true;
					options.Password.RequireUppercase = true;
					options.Password.RequiredLength = 6;
					options.Password.RequiredUniqueChars = 1;

					// Lockout settings.
					options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
					options.Lockout.MaxFailedAccessAttempts = 5;
					options.Lockout.AllowedForNewUsers = true;

					// User settings.
					options.User.AllowedUserNameCharacters =
					"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
					options.User.RequireUniqueEmail = false;
				});

				services.ConfigureApplicationCookie(options =>
				{
					// Cookie settings
					options.Cookie.HttpOnly = true;
					options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

					options.LoginPath = "/Identity/Account/Login";
					options.AccessDeniedPath = "/Identity/Account/AccessDenied";
					options.SlidingExpiration = true;
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
					}
					else
					{
						app.UseExceptionHandler("/Error");
						// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
						app.UseHsts();

						pizzaShopDbContext.Database.Migrate();
					}
				} catch (Exception e) {
					throw e;
				}
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
