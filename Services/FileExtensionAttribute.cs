using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    //329.  Extend validation in this class
    public class FileExtensionAttribute : ValidationAttribute
    {
        //330.  Need and override method for if call is valid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //331.  validationContext can call services and get dependencies from dbcontext using following command
            //var context = (HDbContext)validationContext.GetService(typeof(HDbContext));
            //**Notes** now context can call dbcontext such as products class, categories class, etc..


            //332.  If string needs validation, call tostring method.
            //value.ToString();


            //333.  Need to validate a file rather than a string
            var file = value as IFormFile;
            //334.  Need to check if file is not null and check for specific files such as jpg/png
            if(file !=null)
            {
                var extension = Path.GetExtension(file.FileName);

                //335.  Create an array that allow extension
                string[] extensions = { "jpg", "png" };

                //336.  Check if variable contain extension by true and false
                bool result = extensions.Any(x => extension.EndsWith(x));

                //337.  Create an if statement for error if file does not end with jpg/png
                if(!result)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            //339.  If successful, return success
            return ValidationResult.Success;
            //340.  Go back to Image property in Product class

        }

        //338.  Create a method for error message
        private string GetErrorMessage()
        {
            return "File must be jpg and/or png";
        }
    }
}
