using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Customer
    {
        public string Address { get;  set; }
        public string Code { get;  set; }
        public string FullName { get;  set; }
        public string MobileNumber { get;  set; }
    }
}