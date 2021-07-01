using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShopWebApp.Models
{
    public class CartItem
    {
        public Shoe Shoe { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
    }
}
