using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Areas.Admin.Controllers
{
    //779.  Add Authorization attribute, next User Controller
    //704.  Set class to Admin area
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        //705.  Call RoleManager and UserManager Dependency
        private readonly RoleManager<IdentityRole> rManager;
        private readonly UserManager<AppUser> uManager;
        //706.  Create a constructor for this class to use managers
        public RolesController(RoleManager<IdentityRole> rManager, UserManager<AppUser> uManager)
        {
            this.rManager = rManager;
            this.uManager = uManager;
        }

        //707.  Return manager roles, Create Empty Razer View Index Page  GET/admin/roles
        public IActionResult Index()
        {
            return View(rManager.Roles);
        }

        //711.  Create a GET and View page for creation of roles GET/admin/roles/create
        public IActionResult Create()
        {
            return View();
        }

        //711.  Create a POST method for Create POST /admin/roles/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([MinLength(2), Required] string name)
        {
            //712.  Check if Model state is valid
            if(ModelState.IsValid)
            {
                //713.  Call IdentityResult for the Create Async method to create a new role
                IdentityResult result = await rManager.CreateAsync(new IdentityRole(name));

                //714.  Write an if statement to check if a role has been succeeded
                if(result.Succeeded)
                {
                    TempData["Success"] = "A new role has been created";
                    return RedirectToAction("Index");
                }
                //715.  If not, return error 
                else
                {
                    foreach (IdentityError error in result.Errors) ModelState.AddModelError("", error.Description);
                }

            }
            ModelState.AddModelError("", "Minimum Length is 2");
            return View();
        }

        //716.  Create an  GET Edit method for Roles, first Create a RoleEdit Class in Models, go to class
        //723.  Create the GET Edit method GET /admin/roles/edit/id
        public async Task<IActionResult> Edit(string id)
        {
            //724.  Use IdentityRole to get roles by ID from role manager
            IdentityRole role = await rManager.FindByIdAsync(id);

            //725.  Create a list for Members and NonMembers
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();

            //726.  Need to loop through users and add to Members or Nonmembers list using a foreach
            foreach (AppUser user in uManager.Users)
            {
                //727.  Instantiate a variable list to call UserManager to check for roles and see if they're members or nonmembers
                var list = await uManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                //728.  Then add the user
                list.Add(user);
            }
            //728.  Return a new Role from RoleEdit Class and connect the roles
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
            //729.  Create a Razer View Page for Edit, using Edit Template and RoleEdit class
        }

        //739.  Create the POST Edit method POST /admin/roles/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEdit rEdit)
        {
            //740.  Declare IdentityResult
            IdentityResult result;

            //741.  Loop through AddIds using a foreach to add rolle, if it's null create a new array (if it's empty nothing will happen)
            foreach(string userId in rEdit.AddIds ?? new string[] { })
            {
                AppUser user = await uManager.FindByIdAsync(userId);
                result = await uManager.AddToRoleAsync(user, rEdit.RoleName);
            }

            //742.  Loop through AddIds using a foreach to delete roll or an empty array (if it's empty nothing will happen)
            foreach (string userId in rEdit.DeleteIds ?? new string[] { })
            {
                AppUser user = await uManager.FindByIdAsync(userId);
                result = await uManager.RemoveFromRoleAsync(user, rEdit.RoleName);
            }
            return Redirect(Request.Headers["Referer"].ToString());
            //743.  Check browser and add roles to see if succeeded, then check database and see if user role match with roles
            //744.  Create a custom Tag Helper to display which user has what role.
            //745.  Create a RolesTagHelper Class in Services, go to class

        }

    }
}
