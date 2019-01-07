using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Airport.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Airport.API
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
            //Call UnitOfWork to get AuctionRepository
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0.0", new Info
                {
                    Version = "1.0.0",
                    Title = "Airport API",
                    Description = "Airport API Documentation (ASP.NET Core 2.1)",
                    Contact = new Contact()
                    {
                        Name = "Christian Schou",
                        Url = "https://github.com/christian-schou",
                        Email = "chri314x@edu.eal.dk"
                    },
                    TermsOfService = "http://swagger.io/terms/"
                });

                //Determine base path for the application.
                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var fileName = Path.GetFileName(assemblyName + ".xml");

                //Set the comments path for the swagger json and ui.
                //c.IncludeXmlComments(Path.Combine(basePath, fileName));

            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext applicationDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            applicationDbContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Flight, Entities.Models.FlightDto>();
                cfg.CreateMap<Entities.Models.FlightForUpdateDto, Entities.Flight>();
                cfg.CreateMap<Entities.Flight, Entities.Models.FlightForUpdateDto>();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Airport");

                //TODO: Or alternatively use the original Swagger contract that's included in the static files
                // c.SwaggerEndpoint("/swagger-original.json", "Swagger Petstore Original");

            });

            app.UseMvc();
        }
    }
}
