using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    //612.  Extend IdentityUser for this class from aspnetcore.identity
    public class AppUser : IdentityUser
    {
        //613.  Create property for Users
        public string Occupation { get; set; }
    }
    //614.  Need to extend IdentityDbContext in HDbContext for roles 
    //615.  Install nugetpackage Microsoft.AspNetCore.Identity.EFCore
    //616.  Go to HDbContext.cs in services
}
