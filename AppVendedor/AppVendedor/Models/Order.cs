using System;
using System.Collections.Generic;
using System.Text;

namespace AppVendedor.Models
{
    public class Order
    {
        public string KeyOrder { get; set; }
        public string Client { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public int IdSeller { get; set; }
    }
}
