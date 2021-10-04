using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSSAProject.Models;

namespace MSSAProject.Services
{
    //5.  Call DBContext Class
    public class HDbContext : DbContext
    {
        //6.  Create a constructor options to reference class
        public HDbContext(DbContextOptions<HDbContext> options)
            : base(options)
        {
        }

        //7.  Delete Error View Model Class in Models,  HomeController in Controllers, Home Folder, and Error.cshtml to start fresh
        //8.  Create a Class in Models call Page
        //9.  Go to Page Class

        //12.  Add using models to access the class
        //13.  Create a database list to pull properties
        public DbSet<Page> Pages { get; set; }
        //14.  Need to register the service - Go to Startup

        //205.  Add Category table
        public DbSet<Category> Categories { get; set; }
        //206.  Addmigration
        //207.  Check migration folder and there will be another timestamp
        //208.  updatedatabase and see if Categories in SQLdb, check if all properties are not nulled
        //209.  Go to CategoriesController

        //281.  Create table for Products
        public DbSet<Product> Products { get; set; }
        //282.  Add-migration and Update-Databse
        //283.  Check SQLDb to see columns and FKs
        //284.  Go to ProductsController
    }
}
