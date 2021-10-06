using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class User
    {
        //621.  Create properties for users
        [Required, MinLength(2, ErrorMessage = ("Minimum Length is 2"))]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8, ErrorMessage = ("Minimum Length is 8"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //622.  Create an empty constructor and parameter just to call AppUser class
        public User() { }

        public User(AppUser appUser)
        {
            UserName = appUser.UserName;
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
        //623.  Create Account controller and implement authorize attribute
        //624.  Need to specify the use of Identity in Startup.cs in ConfigureServices, go to startup
    }
}
