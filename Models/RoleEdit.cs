using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class RoleEdit
    {
        //717.  Create role properties for this class using IdentityRole
        public IdentityRole Role { get; set; }
        //718.  Create a list property for Members
        public IEnumerable<AppUser> Members {get;set;}
        //719.  Create a List property for NonMembers
        public IEnumerable<AppUser> NonMembers { get; set; }
        //720.  Create a property for role name
        public string RoleName { get; set; }
        //721.  Create a string array for Add and Delete Ids to track
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
        //722.  Go back to RolesController for Post method for Edit
    }
}
