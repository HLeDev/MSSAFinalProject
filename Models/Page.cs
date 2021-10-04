using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class Page
    {
        //10.  Create properties 
        //***Notes*** Id property is automatically recognized as the PK, if the Id name is different, then an attribute of [Key] must be set for the 
        //property to be recognize as the PK
        //11.  Go back to HungDBContext Class

        //30.  We'll now set the attributes for the properties, once an attribute is set, the tables will no longer be Null
        //31.  Once done, redo the migration, go to Startup

        //110.  Add more attributes for validation such as length
        //111.  Test the Browser for validation of field input, minlength, create new page, and same title validation
        //112.  Go back to PagesController, go to Post, and call partial view to flash messages
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Minimum Length is 2")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Minimum Length is 4")]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
