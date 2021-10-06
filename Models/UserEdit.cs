using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class UserEdit
    {
        //674.  Add properties that users will be editing

        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //675.  Create an empty constructor and parameter just in case
        public UserEdit() { }

        public UserEdit(AppUser appUser)
        {
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
        //676.  Go to AccountController
    }
}
