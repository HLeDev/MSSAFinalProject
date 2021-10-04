using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    public class CategoriesViewComponent : ViewComponent
    {
        //488.  Create a private readonly variable for dbcontext then create a public constructor for this class to get dependencies
        private readonly HDbContext context;
        public CategoriesViewComponent(HDbContext context)
        {
            this.context = context;
        }
        //489.  Use invoke async to display pages
        //490.  Create a method to invoke async to return view
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //491.  Create GetCategoriesAsync method and generate the method for it
            var cats = await GetCategoriesAsync();
            return View(cats);
        }

        //491.  Modify this method to get list of pages
        private Task<List<Category>> GetCategoriesAsync()
        {
            return context.Categories.OrderBy(x => x.Sorting).ToListAsync();
        }
        //492.  Create view page for component
        //493.  Create a Folder in Shared/Components call Categories, then add a Razer View Page call Default
    }
}

