using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSSAProject.Models;
using MSSAProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Areas.Admin.Controllers
{
    //778.  Add Authorization attribute, next Roles Controller
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        //275.  Call database context class
        private readonly HDbContext context;

        //352.  Add dependency for image upload using webhost and add it to this class constructor
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(HDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            
        }
        //276.  Create a class call Product in Models Folder
        //277.  Go to Product Class


        //285.  Make Index async to get categories GET /admin/products
        public async Task<IActionResult> Index(int p = 1)
        {
            //364.  Create a variable with a number to display number of products per page
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                .Include(x => x.Category)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);
            //365.  Find package call Pagination to do page translation, get code for page number display on browser
            //367.  Create a class call PaginationTagHelper to paste the tag helpers from codehaks.pagination
            //368.  Once that's done, add the taghelper in _ViewImports.cshtml
            //369.  Go to Products Index page and add the pagination for page count

            //372.  Add viewbag
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);
            //373.  Go back to Index and call viewbag and edit tag helper to point the pages
            //374.  Go to PaginationTagHelper class to modify routes


            //286.  Get products by descending through Ids and include Category
            return View(await products.ToListAsync());
            //287.  Create a Razer View Index Page for Products
        }

        //293.  Create a method for create GET /admin/products/create
        [HttpGet]
        public IActionResult Create()
        {
            //294.  Need pass categories to this view for when product is created, it show category for the product
            //295.  Use SelectList method to select product by categoryid given id and name
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }
        //296.  Create a Razier View Page for Create method, go to Create Page


        //343.  Create a Post method for products create POST /admin/products/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            //362.  Create a viewbag to display categories if ckeditor is disable
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name");
            //363.  Go to Index method to addjust number of products displayed per page

            //344.  Get formdata via model binding by inserting (Product product) in the create method
            //345.  Need to check if model state is valid using and if statement
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                //346.  Need to check if the title or slug exist
                var slug = await context.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists");
                    return View(product);
                }

                //352.  Need to upload the image now
                string imageName = "NoImg.png";
                //**Notes** noimage is now default image incase no image is uploaded
                //353.  Create a new folder call media in root folder and then create a products folder in media to contain images
                //354.  Add a default no image pic
                //355.  Use webhost to get path
                //356.  Write an if statement to see if image is null or not and pull images from root path
                if (product.ImageUpload != null)
                {
                    string upload = Path.Combine(webHostEnvironment.WebRootPath, "media/products");

                    //357.  use Guid for unique identifier of images to make sure it doesnt get added twice and overriden
                    imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    //358.  Now to get complete file path of the image
                    string filePath = Path.Combine(upload, imageName);

                    //359.  Now for the upload of image, use filestream to add new image
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                //360.  Now to save the image uploaded
                product.Image = imageName;
                //361.  Go to browser create product page and add products


                //347.  If everything is valid, Add the page
                context.Add(product);
                await context.SaveChangesAsync();

                //348.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The product has been created!";

                //349.  Post the product and return to index page
                return RedirectToAction("Index");
            }
            //350.  If the modelstate is not valid, return the page
            return View(product);
            //351.  Need to upload the image, need to add another dependency to ProductController
        }


        //380.  Create a Detail method for products GET/admin/products/details
        public async Task<IActionResult> Details(int id)
        {
            //381.  Get specific id of the product
            Product product = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            //382.  Write an if statement to check if product is found or not
            if (product == null)
            {
                return NotFound();
            }
            //383.  Otherwise, return view
            return View(product);
            //384.  Add Details Razer View Page to view products
            
        }

        //392.  Create a method for Edit GET /admin/products/edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //392.  Get specific product by ID
            Product product = await context.Products.FindAsync(id);

            //393.  Write an if statement to verify if page is null or not and returning errors
            if (product == null)
            {
                return NotFound();
            }
            //394.  If found, return the page and add viewbag to select category
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name", product.CategoryId);
            return View(product);
            //395.  Create a Razer View Page for Edit
            //396.  Go to Edit Page
        }

        //411.  Create a Post method for products edit POST /admin/products/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, int id)
        {
            //412.  Create a viewbag to display categories if ckeditor is disable
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name", product.CategoryId);

            //413.  Need to check if model state is valid using and if statement
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                //414.  Need to check if the title or slug exist
                var slug = await context.Products.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists");
                    return View(product);
                }

                //415.  Write an if statement to see if image is null or not and pull images from root path
                if (product.ImageUpload != null)
                {
                    string upload = Path.Combine(webHostEnvironment.WebRootPath, "media/products");

                    //416.  Need to check if product img is equal to noimage picture, dont remove
                    if (!string.Equals(product.Image, "NoImg.png"))
                    {
                        string oldImage = Path.Combine(upload, product.Image);
                        //417.  Write an if statement to delete old image
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }
                    //418.  use Guid for unique identifier of images to make sure it doesnt get added twice and overriden
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    //419.  Now to get complete file path of the image
                    string filePath = Path.Combine(upload, imageName);

                    //420.  Now for the upload of image, use filestream to add new image
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    //***Note*** update new image into the db
                    product.Image = imageName;

                }

                //421.  If everything is valid,update the product
                context.Update(product);
                await context.SaveChangesAsync();

                //422.  Use Tempdata to flash messages from partial view
                TempData["Success"] = "The product has been edited!";

                //433.  Post the product and return to index page
                return RedirectToAction("Index");
            }
            //444.  If the modelstate is not valid, return the page
            return View(product);
        }


        //445.  Create a method for Delete
        //446.  Use GET > admin > products > delete > id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //447.  Need to find the Id
            Product product = await context.Products.FindAsync(id);

            //448.  Check if Id is null
            if (product == null)
            {
                TempData["Error"] = "The Page does not exist";
            }
            //449.  If it is not null
            else
            {
                //450.  Need to delete image along with content
                if (!string.Equals(product.Image, "NoImg.png"))
                {
                    string upload = Path.Combine(webHostEnvironment.WebRootPath, "media/products");
                    string oldImage = Path.Combine(upload, product.Image);
                    //417.  Write an if statement to delete old image
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }
                context.Products.Remove(product);
                await context.SaveChangesAsync();

                TempData["Success"] = "The product has been deleted";
            }

            //451.  If found, redirect back to index
            return RedirectToAction("Index");
        }
        //452.  Done with Admin area, Now for FE User, Create a new Controller  in Controllers Folder
        //453.  Create a new class call MainMenuViewCompnent in Services
    }
}
