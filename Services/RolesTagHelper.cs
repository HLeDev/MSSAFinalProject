using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    //746.  Extend TagHelper
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RolesTagHelper : TagHelper
    {
        //747.  Inject Role and User Manager Dependencies
        private readonly RoleManager<IdentityRole> rManager;
        private readonly UserManager<AppUser> uManager;
        public RolesTagHelper(RoleManager<IdentityRole> rManager, UserManager<AppUser> uManager)
        {
            this.rManager = rManager;
            this.uManager = uManager;
        }

        //748.  Custom tag helper get depencies from constructor same as controller
        //749.  Create a property to target user-role attribute from Role Index page
        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }
        //750.  IOT target specific tags, a target element attribute must be set for the class, set it and what to target

        //751.  Create a Process Async method to call tag helper context and output to push out strings, same as Pagination
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //752. Create a list of names
            List<string> names = new List<string>();
            //753.  Call Identity role to user Role Manager to find by id with the property of RoleId from role-name
            IdentityRole role = await rManager.FindByIdAsync(RoleId);

            //754.  Check if role is  not
            if (role != null)
            {
                //755.  Create a foreach loop through all the users and see if any is in a specific role
                foreach(var user in uManager.Users)
                {
                    if(user != null && await uManager.IsInRoleAsync(user, role.Name))
                    {
                        //756.  Add to the name list
                        names.Add(user.UserName);
                    }
                }
            }
            //757.  Set the ouput to set content if there's a user then join string
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
        //758.  Now to lock down admin controller for admin only, editor to edit pages controller
        //759.  Open up all Controllers and set authorization, starting with CategoriesController
        


    }
}
