using Microsoft.AspNetCore.Mvc;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Services
{
    //561.  Extend ViewComponent
    public class SmallCartViewComponent : ViewComponent
    {
        //562.  Create an invoke view method to display result

        public IViewComponentResult Invoke()
        {
            //563.  Need to get session data
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            //564.  Need to declare SmallCartViewModel and then check for null
            SmallCartViewModel scVM;

            //565.  Need to check cart for null or empty
            if (cart == null || cart.Count == 0)
            {
                scVM = null;
            }
            else
            {
                //566.  If not null, get number of items and total price
                scVM = new SmallCartViewModel
                { 
                    NumberofItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(scVM);
            //567.  Create  a new Folder in Shared/Components call SmallCart for page view, then create a default page, go to page
        }

    }
}
