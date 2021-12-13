using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class JobPart
    {
        public long Code { get; set; }
        public String JobCode { get; set; }

        public String ProductCode { get; set; }
        public String ProductModel { get; set; }
        public String ProductDescription { get; set; }

        public double ProductPriceLBP { get; set; }
        public double ProductPriceUSD { get; set; }

        public int Quantity { get; set; }
        public DateTime IssueDate{ get; set; }

        
    }
}