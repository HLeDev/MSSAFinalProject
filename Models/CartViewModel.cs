using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class CartViewModel
    {
        //514.  Create 2 properties for list of cart item and grand total
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
        //515.  Go back to CartController
    }
}
