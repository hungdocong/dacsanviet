using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dacsanviet.Models.DTO
{
    public class CartDTO
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}