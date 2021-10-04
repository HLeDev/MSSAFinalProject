using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Models
{
    public class CartItem
    {
        //507.  Create properties for cart items
        public int ProductId { get; set; }
        public string ProductName { get; set;}
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }

        //508.  Now we need to get data from Product Model
        //509.  Create 2 constructors for cart item, 1 empty and 1 with properties pulling properties from Product class
        public CartItem()
        {

        }
        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Quantity = 1;
            Price = product.Price;
            Image = product.Image;
        }
        //510.  Go to CartController Index method

    }

}
