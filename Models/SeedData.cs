using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class SeedData
    {
        //37.  Add using services , EFCore, and dependencyinjections
        //38.  Create a constructor to initialize the service provider to register services
        public static void Initialize(IServiceProvider serpro)
        {
            using (var context = new HDbContext
                (serpro.GetRequiredService<DbContextOptions<HDbContext>>()))
            {
                //39.  Write an if statement check if there's any rows in the pages, if there are any return nothing
                if (context.Pages.Any())
                {
                    return;
                }
                //40.  Otherwise, Add ranges of rows to the pages
                context.Pages.AddRange(
                    new Page
                    {
                        Title = "Home",
                        Slug = "home",
                        Content = "home page",
                        Sorting = 0
                    },
                    //**Notes*** slug is url friendly representation of a title
                    new Page
                    {
                        Title = "About",
                        Slug = "about-us",
                        Content = "about us page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Services",
                        Slug = "services",
                        Content = "services page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Contact",
                        Slug = "contact",
                        Content = "contact page",
                        Sorting = 100
                    }
                    );
                context.SaveChanges();
                //41.  Go to Program.cs
            }
        }
    }
}
