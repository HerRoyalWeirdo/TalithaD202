using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ExploreCalifornia.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

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
            services.AddTransient<FeatureToggles>(x => new FeatureToggles{ 
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });

            // services.AddMvc();
            //https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-5.0&tabs=visual-studio#use-mvc-without-endpoint-routing
            // http://localhost:53934/home/index //!is working!no work!
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //for Db - BlogDataContext
            services.AddDbContext<BlogDataContext>(options =>
            {
                var connString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connString);
            });
        }
        //https://www.koskila.net/solving-dbcontextoptionsbuilder-does-not-contain-a-definition-for-usesqlserver/

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
                //do i need to be developer mode thingy?
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });
            //mvc
            app.UseMvc(routes => {routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
            //middleware
            app.UseFileServer();
            //when i run just says "hello testing ASP.NET Core MVC" 12/09
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