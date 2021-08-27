using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(x => new FeatureToggles{ DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperException")
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //  if (configuration["EnableDeveloperExceptions"] == "True")
        //env.IsDevelopment()
        // if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
        //why this breakpoint not show up?
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");

            if (features.DeveloperExceptions)//my dependency injection is not working
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });
            //middleware
            app.UseFileServer();

        }
    }
}
    //basics - 4 for erroe custom page
      //app.UseRouting();

    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapGet("/", async context =>
    //    {
    //        await context.Response.WriteAsync("Hello Nothing!");//chganging then savimng then reloading without re runnign avctually works
    //        //ctrl + f5 for no debugging
    //    });
    //});