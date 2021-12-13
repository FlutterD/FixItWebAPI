using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Visit
    {
        public long Code { get; set; }
        public long JobTechCode { get; set; }

        public DateTime Date { get; set; }
        public DateTime LastUpdated { get; set; }

        public JobTech JobTech { get; set; }


        public String CustomerFullName { get; set; }
        public String CustomerPhoneNumber { get; set; }
        public String CustomerCode { get;  set; }
    }
}