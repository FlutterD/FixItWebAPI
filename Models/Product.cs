using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Product
    {
        public String Code { get; set; }
        public String Brand { get; set; }
        public String ModelNumber { get; set; }
        public String Description { get; set; }
        public Double PriceLBP { get; set; }
        public Double PriceUSD { get; set; }
    }
}