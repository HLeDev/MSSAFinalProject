using Microsoft.AspNetCore.Http;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class Product
    {
        //278.  Create properties for product class with attributes
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum Length is 2")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum Length is 4")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        
        public string Image { get; set; }

        //279.  Create a relationship between product and category and need to specify FK for Cat table using FK attribute
        //326.  Add range attribute
        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "A category must be chosen")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        //280.  Go to Services > HDbContext Class and create a table for Products


        //311.  Create another property for image using iformfile, need to make sure it doesnt have anything to do with the table
        //327.  Add custom validation for image, Create a class in Services for fileextension
        //328.  Go to FileExtensionAttribute class
        //341.  Call the fileextension attribute
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        //312.  Go to Product Create.cshtml
        //342.  Now to create a Post method to validate the request, Go to ProductsController


    }
}
