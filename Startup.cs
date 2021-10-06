using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSSAProject.Models;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject
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
            //520.  Call addmemorycache and add session
            services.AddMemoryCache();
            services.AddSession();
            //options => { options.IdleTimeout = TimeSpan.FromDays(1); }
            //521.  Go to Configure Method to use session

            //786.  Call routing options services for lower url
            services.AddRouting(options => options.LowercaseUrls = true);
            //787.  


            services.AddControllersWithViews();

            //1.  Install Nuget Packages: EntityFrameworkCore - SQL Server / Tools
            //2.  Create a Folder call Services for Classes
            //3.  Create a Class call HDbContext
            //4.  Go to HDbContext Class

            //15.  Add using MS EFCore & Models & Services
            //16.  Use Lamda Expression to add DBContext services, use the contextclass, use SqlServer, then give the db a name
            services.AddDbContext<HDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HDbContext")));
            //***Note*** This is called register services and dependencies
            //17.  Need to add ConnectionStrings to appsetting.json
            //18.  Go to appsetting.json

            //22.  Migrate the DB to SQLdb
            //23.  Go to Tools > NuGet Package Manager > Package Manager Console
            //24.  Clear the console using - Clear
            //25.  Add the migration with the command "Add-Migration" then name of the migration (Add-Migration InitialCreate)
            //26.  A Migrations Folder is automatically created upon pressing enter
            //27.  Go to InitialCreate Class in Migrations Folder
            //28.  We'll see that a Method called "Up" is automatically created for the Table, and our Classes with properties is also there,
            //Right now, the strings and int are nullable, so we'll delete the whole entire Migrations Folder

            //29.  Go to Page Class in Models

            //32.  Re Add-Migration
            //33.  Go to the InitialCreate Class and verify that the properties is no longer null
            //33.  We'll now update the databse using the command (Update-Databse)
            //34.  Go to View > SQL Server Object Explorrer
            //35.  Right Click dbo.Pages and view designer and verify that the rows are based on the HPProduct class
            //36.  Create a SeedData class in Models and go to class



            //637.  Add options in AddIdentity for requirements of password
            //638.  Check browser and see if it will redirect to login page, then check sql database to see if data is in there
            //639.  Create a Login Class in Model Folder, go to class
            //625.  Use services for identity
            services.AddIdentity<AppUser, IdentityRole>(options => { 
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            
            }).AddEntityFrameworkStores<HDbContext>().AddDefaultTokenProviders();
            //626.  Go to configure method to use authentication



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //522.  Call Use Session method then check if cart browser is working properly
            app.UseSession();
            //523.  Go to CartController and create a cart method


            //627.  Use Authentication
            app.UseAuthentication();
            //628.  Create Account Controller to serve for registering logging in and editing users
            //629.  Create a Controller in Controllers call AccountController

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                //460.  Create another endpoint 
                //**Notes** for route, list them from most specific to least specific
                endpoints.MapControllerRoute(
                    "pages",
                    "{slug?}",
                    defaults: new { controller = "Pages", action = "Page" });

                //481.  Create route for product category page
                endpoints.MapControllerRoute(
                    "products",
                    "products/{categorySlug}",
                    defaults: new { controller = "Products", action = "ProductsByCategory" });
                //482.  Create empty razer View page for action

                //52.  Inject the Admin Area route here and change route to endpoint.mapcontrollerroute, then change template to pattern
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );
                //53.  Check the Areas Folder, and see sub folders, delete Models and Data as we'll be using the main content
                //54.  Now add a Controller called Pages in Areas>Admin>Controllers Folder
                //55.  Go to PagesController Class


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                
            });
        }
    }
}
