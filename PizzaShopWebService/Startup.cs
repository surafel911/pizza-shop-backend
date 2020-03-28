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

using Npgsql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PizzaShopWebService.Data;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (PizzaShopDbContext pizzaShopDbContext = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<PizzaShopDbContext>()) {
                try {
                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        
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
