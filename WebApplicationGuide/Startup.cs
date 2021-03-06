using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApplicationGuide.Models;

namespace WebApplicationGuide
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(
                    "CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            //Если мы поменяли модель и хотим обновить базу
            //dotnet ef migrations remove --context ElectricityСountContext //в том случае если что то осталось
            //dotnet ef migrations add InitialCreate --context ElectricityСountContext
            //dotnet ef database update --context ElectricityСountContext

            services.AddDbContext<ElectricityСountContext>(optionsBuilder =>
                optionsBuilder.UseSqlite("Data Source=DemoProject.db;Cache=Shared"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            
            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}