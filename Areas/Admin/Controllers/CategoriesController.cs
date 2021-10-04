using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSSAProject.Models;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        //200.  inject dbcontext depencies and make area admin above class
        private readonly HDbContext context;
        public CategoriesController(HDbContext context)
        {
            this.context = context;
            //201.  Now we can use this context in other methods
            //202.  Create a Category class in Model, go to Category.cs
        }

        //210.  Make Index async to get categories GET /admin/categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //211.  Get all of the categories 
            return View(await context.Categories.OrderBy(x => x.Sorting).ToListAsync());
            //212.  Create View for Category and go to Index
        }

        //217.  Create a method to get create page GET admin/categories/create
        [HttpGet]
        public IActionResult Create() => View();
        //218.  Create Razer View Page and select Category class as model class
        //219.  Go to Categories Create Page

        //225.  Create post method POST /admin/categories/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category cat)
        {
            //226.  Need to check if model state is valid using and if statement
            if (ModelState.IsValid)
            {
                cat.Slug = cat.Name.ToLower().Replace(" ", "-");
                cat.Sorting = 100; //227.  Sorting will place any new category created to the very end

                //228.  Need to check if the title or slug exist, so we dont have 2 categories with the same name
                var slug = await context.Categories.FirstOrDefaultAsync(x => x.Slug == cat.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title's already existed");
                    return View(cat);
                }

                //229.  If everything is valid, Add the category
                context.Add(cat);
                await context.SaveChangesAsync();

                //230.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The category has been added!";
                
                //231.  Post the category and return to index page
                return RedirectToAction("Index");
            }
            //232.  If the modelstate is not valid, stay in the same page
            return View(cat);
            //233.  Validate of a category can be added
        }


        //234.  Create a method to edit the categories by id GET /admin/categories/edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category cat = await context.Categories.FindAsync(id);
            if(cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }
        //235.  Create razer view page for Edit, go to Edit.cshtml

        //239.  Create a post method for edit POST /admin/categories/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category cat)
        {
            //240.  Need to check if Model is valid
            if (ModelState.IsValid)
            {

                //241.  Then need to check compare and check slug and name for case sensitive
                cat.Slug = cat.Name.ToLower().Replace(" ", "-");

                //242.  Need to check if the slug exist for any other page besides this
                var slug = await context.Pages.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == cat.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already existed");
                    return View(cat);
                }

                //243.  If everything is valid, update the page
                context.Update(cat);
                await context.SaveChangesAsync();

                //244.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The category has been updated!";

                //245.  Post the page and return to Edit page, this redirect need the Id from GET
                return RedirectToAction("Edit", new { id });

            }
            //246.  If the modelstate is not valid, return the page
            return View(cat);
            //247.  Test Edit post method in browser
            //248.  Once edit is functioning properly, create delete method to delete category
        }


        //249.  Create a method for Delete
        //250.  Use GET > admin > categories> delete > id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //251.  Need to find the Id
            Category cat = await context.Categories.FindAsync(id);

            //252.  Check if Id is null
            if (cat == null)
            {
                TempData["Error"] = "The category does not exist";
                //**to test if cat exist, go to /admin/categories/delete/"randomname" and see the error message
            }
            //253.  If it is not null
            else
            {
                context.Categories.Remove(cat);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been deleted";
            }

            //254.  If found, redirect back to index
            return RedirectToAction("Index");
            //255.  Test Delete post method in browser
            //256.  Now create Sorting function for categories
            //257.  Go to Categories index view and give table class a sorting
        }


        //267.  Create a method for Reorder,POST /admin/categories/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            //268.  Create a variable count and assign id of 1 to begin
            int count = 1;
            //269.  Create foreach loop to reorder ID base on the location
            foreach (var categoryId in id)
            {
                Category cat = await context.Categories.FindAsync(categoryId);
                cat.Sorting = count;
                context.Update(cat);
                await context.SaveChangesAsync();
                count++;
            }
            //270.  Return status 200 ok which is IActionResult
            return Ok();
            //271.  Verify sorting is functioning properly by verifying the browser script and sqldb
        }
        //272.  Now to create a Products Model and Controller
        //273.  Create a ProductsController.cs in Admin > Controllers
        //274.  Go to Products Controller
    }
}
