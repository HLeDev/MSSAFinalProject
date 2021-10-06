using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSSAProject.Models;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        //466.  Implement constructor for product donctex
        private readonly HDbContext context;
        public ProductsController(HDbContext context)
        {
            this.context = context;
        }

        //467.  Implement async task for index to pull pages
        public async Task<IActionResult> Index(int p = 1)
        {
            //468.  Create a variable with a number to display number of products per page
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            //469.  Add viewbag for pagination usage
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);

            return View(await products.ToListAsync());
            //470.  Create an empty Razer View Index Page for Products without template and use a layout page
        }

        //475.  Implement async task method to pull products by category GET /products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug,int p = 1)
        {
            //476.  Import category and pull categories by slug to not run into capitalization
            Category cat = await context.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();
            //477.  Write an if statement to check if category exist, if it doesnt, go back to index
            if (cat == null)
            {
                return RedirectToAction("Index");
            }

            //478.  Create a variable with a number to display number of products per page by pulling category by it's ID
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                .Where(x => x.CategoryId == cat.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            //479.  Add viewbag for pagination usage
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Where(x => x.CategoryId == cat.Id).Count() / pageSize);
            ViewBag.CategoryName = cat.Name;
            @ViewBag.CategorySlug = categorySlug;
            //480.  Create a route for product category page, go to Startup

            return View(await products.ToListAsync());
        }
    }
}
