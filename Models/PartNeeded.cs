using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class PartNeeded
    {
        public long Code { get; set; }
        public String ProductCode { get; set; }
        public int Quantity { get; set; }
        public String Comment { get; set; }
        public String JobCode { get; set; }
    }
}