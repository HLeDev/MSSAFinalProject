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
    public class PagesController : Controller
    {
        //453.  Create a private readonly variable for dbcontext then create a public constructor for this class
        private readonly HDbContext context;
        public PagesController(HDbContext context)
        {
            this.context = context;
        }
        //454.  Change Index method to async GET / or / slug
        public async Task<IActionResult> Page(string slug)
        {
            //455.  Write and if statement to check if slug is null, if it is, return to homepage
            if(slug == null)
            {
                return View(await context.Pages.Where(x => x.Slug == "home").FirstOrDefaultAsync());
            }

            Page page = await context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();
            //456.  Write an if statement return notfound page is page is null
            if(page == null)
            {
                return NotFound();
            }

            return View(page);
            //457.  Create a new Razer View Page for Page
        }
    }
}
