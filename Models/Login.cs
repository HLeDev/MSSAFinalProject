using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class Login
    {
        //640.  Create properties for users to log in with email address
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8, ErrorMessage = ("Minimum Length is 8"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //641.  Create a return url property for return query string when user attemp to access authorized pages
        public string ReturnUrl { get; set; }

        //642.  Go to Account Controller and create a login method and view
    }
}
