using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Company
    {
        public String CompanyName { get; set; }
        public String BranchName { get; set; }
        public String CompanyAddress { get; set; }
        public String VATReference { get; set; }
    }
}