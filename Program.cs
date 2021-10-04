using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //42.  Create a var for CreateHostBuilder for building only and not run
            var host = CreateHostBuilder(args).Build();

            //43.  Add a using of Scopes and initialize the services using try catch block
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //44.  Run the host
            host.Run();
            //45.  Run the project and verify the 404 error page because Home Folder and controller was deleted
            //46.  Go to SQL Server Object Explorer > Databses > HungFinalProject > Tables > dbo.HProducts > View Data
            //47.  Verify the SeedData class was succesfully implemented in the database
            //48.  Now to create an Admin Section
            //49.  Create a folder call Areas and add an MVC Area call "Admin"
            //50.  IOT use the admin area, we have to add a route to it!
            //51.  Copy the Route from the Admin Area > Go to Startup
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
