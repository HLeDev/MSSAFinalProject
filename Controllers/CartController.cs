using Microsoft.AspNetCore.Mvc;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    public class CartController : Controller
    {
        //500.  Create a private readonly variable for dbcontext then create a public constructor for this class to get dependencies
        private readonly HDbContext context;
        public CartController(HDbContext context)
        {
            this.context = context;
        }

        // Get /cart
        public IActionResult Index()
        {
            //511.  Need to make method synchronous, pull list from cart item and see if it's in session or not, if not create a new list
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            //512.  Either get list from session, if it's null, create a new list
            //513.  Create a CartViewModel Class in Models

            //516.  Create a variable for CartViewModel class to get items and sum
            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            //517.  Create a Razer View Page for Index Page, using Details Template and CartViewModel model class
            return View(cartVM);
        }

        //501.  Create a class call session extension for complex data in session since aspcore only allow int and string in sesssion
        //502.  Create a class call SessionExtension, go to class

        //506.  Create a class call CartItem in Models to represent 1 product in the cart, the cart will be a list of items


        // Get /cart/add/id
        //524.  Create an async task method for Add
        public async Task<IActionResult> Add(int id)
        {
            //525.  Need to get product
            Product product = await context.Products.FindAsync(id);
            //526.  Need to get list of cart item from session or createa new one
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //527.  Need to get particular cart item
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            //528.  Need to check if cart is null using an If statement
            if(cartItem == null)
            {
                //529.  If null, add item to cart
                cart.Add(new CartItem(product));
                //530.  This is when the CartItem constructor come in
            }
            else
            {
                //531.  If an item does exist, increment an Item by 1
                cartItem.Quantity += 1;
            }

            //533.  Need to set the session for the cart and serialize it
            HttpContext.Session.SetJson("Cart", cart);

            //588.  Need to invoke method on cart page with ajax request
            //589.  Need to write an if statement to check the request, if it's not an ajax request, go to index, if it is, return view component
            if(HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");
            
            //590. Go to Clear Method

            return ViewComponent("SmallCart");
            //534.  Go to Products Index page to modify Add to cart button
        }


        // Get /cart/decrease/id
        //544.  Create a method for decrease
        public IActionResult Decrease(int id)
        {
            //545.  Need to get list of cart item from session or createa new one
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            //546.  Need to get particular cart item
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            //547.  Need to check if item quantity is greater than 1, if it is, decrease it
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                //548.  If an itemis 1, remove it
                cart.RemoveAll(x => x.ProductId == id);
            }

            //549.  Need to verify if there's any item at all
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                //550.  Need to set the cart session again
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
            //551.  Go to Products Index page to modify Add to cart button
        }


        // Get /cart/remove/id
        //552.  Create a method for remove
        public IActionResult Remove(int id)
        {
            //553.  Need to get list of cart item from session or createa new one
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            //554.  Need to verify if there's any item at all
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                //555.  Need to set the cart session again
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // Get /cart/clear
        //556.  Create a method to clear cart
        public IActionResult Clear()
        {
            //591.  Modify this method with different redirection to action method in a different controller such as ("Page", "Pages)
            // or return Redirect("/") will take back to home page
            // or return Redirect(Request.Headers["Referer].ToString()) to go back to previous request

            HttpContext.Session.Remove("Cart");
            //592.  Go to www.paypal.com/us/smarthelp/article/how-do-i-add-paypal-checkout-to-my-custom-shopping-cart-ts1200
            //to get method to add paypal to shopping cart

            //593.  Create a Razer Partial View Page in Views > Cart for Paypal then copy form from link

            //609.  Insert ajax check from Add method here
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return Ok();
            //610.  Now to start Identity for managing users, registering, and roles
            //611.  Create a call in Models call AppUser, go to Class
        }
        //557.  Need to create SmallCartView model and component to have smallcart overview below categories to display total number of product and amount
        //558.  Create a new class in Models call SmallCartViewModel, go to class and create property
    }
}
