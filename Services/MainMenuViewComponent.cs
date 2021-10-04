using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    public class MainMenuViewComponent : ViewComponent
    {
        //454.  Create a private readonly variable for dbcontext then create a public constructor for this class to get dependencies
        private readonly HDbContext context;
        public MainMenuViewComponent(HDbContext context)
        {
            this.context = context;
        }
        //455.  Use invoke async to display pages
        //456.  Create a method to invoke async to return view
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //457.  Call GetPagesAsync method and generate the method for it
            var pages = await GetPagesAsync();
            return View(pages);
        }

        //458.  Modify this method to get list of pages
        private Task<List<Page>> GetPagesAsync()
        {
            return context.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }
        //459.  Create view page for component
        //460.  Createa Folder in Shared call Components, then create MainMenu Folder, then add a Razer View Page call Default
    }
}
