using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Areas.Admin.Controllers
{
    //780.  Add Authorization attribute,  Now to modify the tool menu to display logo for admins edit logout in  _Layout for admin
    //687.  Make class an admin area Area
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        //688.  Call UserManager Dependency
        private readonly UserManager<AppUser> uManager;
        
        public UsersController(UserManager<AppUser> uManager)
        {
            this.uManager = uManager;
        }

        public IActionResult Index()
        {
            //689.  Call all the Users
            return View(uManager.Users);
            //690.  Create an Index Razer View Page, List Template, and User Model Class
        }
    }
}
