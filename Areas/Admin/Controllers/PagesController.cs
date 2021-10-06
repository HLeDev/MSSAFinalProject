using Microsoft.AspNetCore.Authorization;
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
    //777.  Add Authorization attribute, next Products Controller
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PagesController : Controller
    {
        //60.  create a private readonly variable for dbcontext then create a public constructor for this class
        private readonly HDbContext context;
        public PagesController(HDbContext context)
        {
            this.context = context;
            //61.  Now we can use this context in other methods
        }

        //56.  Change Index method from IActionResult to string so we can test and replace the method View() with "test"

        //62.  Switch Index method from string back to IActionResult and making it an async task
        public async Task<IActionResult> Index()
        {
            //63.  Add using Models
            //64.  Use IQueryable to call the product
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;

            //65.  Create a list of product pages
            List<Page> hList = await pages.ToListAsync();

            //66.  Then return list
            return View(hList);
            //67.  Now to create the view for this Index Page
            //68.  Right click in this class and add razer View page
            //69.  Select List Template> Page Class
            //70.  Go to Admin Index Page
            //71.  View the page admin/hproduct
            //72.  Copy Share Folder, and _ViewImports/Start to the Views Folder in Admin
            //73.  Verify the page /admin/hproduct
            //74.  Go to Admin Shared Layout Page
        }
        //57.  Inject Area attribute above the class
        //58.  Check routes /admin/page

        //59.  Now to inject dependencies by creating a constructor


        //85.  Do Get admin > pages> details > id number
        public async Task<IActionResult> Details(int id)
        {
            //86.  Now to get specific page
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            //87.  Write an if statement to check if page is found or not
            if(page == null)
            {
                return NotFound();
            }
            //88.  Otherwise, return page
            return View(page);
            //89.  Right click inside this Controller and Add an MVC View Page
            //90.  Go to Details.cshtml page
        }

        //94.  Do GET admin > pages > create
        [HttpGet]
        public IActionResult Create() => View();

        //101. do POST admin > pages > create
        //124.  Add an attribute to validate token, ALWAYS do this when hosting
        //125.  Go to Index View Page and delete delete the delete function from Home Page because we dont want home page to be deleted
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            //102.  Get formdata via model binding by inserting (Page page) in the create method
            //103.  Need to check if model state is valid using and if statement
            if(ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100; //104.  Sorting will place any new page created to the very end

                //105.  Need to check if the title or slug exist
                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title's already existed");
                    return View(page);
                }

                //106.  If everything is valid, Add the page
                context.Add(page);
                await context.SaveChangesAsync();

                //113.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The Page has been created!";
                //114.  Create a Partial View to access the tempdata
                //115.  Go to Share Folder in Areas, Create an MVC Razer View call _Notification
                //116.  Go to _Nofication cshtml



                //107.  Post the page and return to index page
                return RedirectToAction("Index");
            }
            //108.  If the modelstate is not valid, return the page
            return View(page);
            //109.  Now to add the validation in the Page class in Models Folder, go to class
        }



        //128.  Create a function for edit
        //129.  Use GET > admin > pages> edit > id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //130.  Get specific page by ID
            Page page = await context.Pages.FindAsync(id);

            //131.  Write an if statement to verify if page is null or not and returning errors
            if(page == null)
            {
                return NotFound();
            }
            //132.  If found, return the page
            return View(page);
            //133.  Create a Razer View Page for Edit
            //134.  Go to Edit Page
        }

        //140. do POST admin > pages > edit
        //141.  Add an attribute to validate token, ALWAYS do this when hosting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            //142.  Need to check if Model is valid
            if (ModelState.IsValid)
            {

                //143.  Then need to check if Home page is being edited, if it is, need to make sure it's not edible
                page.Slug = page.Id == 1 ? "home" : page.Slug = page.Title.ToLower().Replace(" ", "-");

                //144.  Need to check if the slug exist for any other page besides this
                var slug = await context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title's already existed");
                    return View(page);
                }

                //145.  If everything is valid, update the page
                context.Update(page);
                await context.SaveChangesAsync();

                //146.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The Page has been updated!";

                //147.  Post the page and return to Edit page, this redirect need the Id from GET
                return RedirectToAction("Edit", new { id = page.Id });

                //148.  Now to create the Delete Method
            }
            //108.  If the modelstate is not valid, return the page
            return View(page);
            //109.  Now to add the validation in the Page class in Models Folder, go to class
        }


        //149.  Create a method for Delete
        //150.  Use GET > admin > pages> delete > id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //151.  Need to find the Id
            Page page = await context.Pages.FindAsync(id);

            //152.  Check if Id is null
            if (page == null)
            {
                TempData["Error"] = "The Page does not exist";
            }
            //153.  If it is not null
            else
            {
                context.Pages.Remove(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Page has been deleted";
            }

            //154.  If found, redirect back to index
            return RedirectToAction("Index");
            
            //155.  Need to add the Error portion in _Notification
            //156.  Go to _Notification in Areas > Views > Shared
        }


        //193.  Create a method for Reorder, /admin/pages/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            //194.  Create a variable count and assign id of 1 to begin, since home is 0
            int count = 1;
            //195.  Create foreach loop to reorder ID base on the location
            foreach(var pageId in id)
            {
                Page page = await context.Pages.FindAsync(pageId);
                page.Sorting = count;
                context.Update(page);
                await context.SaveChangesAsync();
                count++;
            }
            //196.  Return status 200 ok which is IActionResult
            return Ok();
            //197.  Open browser, sort the list, check sqlserver and see if sorting id changed, if it does, then reorder method is working
            //198.  Now to create Categories Controller, model, and Migration
            //199.  Go to CategoriesController
        }
    }
}
