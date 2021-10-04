using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class Category
    {
        //203.  Create properties for category and add Attribute for validation
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum Length is 2")]
        [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set;}
        public string Slug { get; set; }
        public int Sorting { get; set; }
        //204.  Add this class to the context, go to HDbContext
    }
}
