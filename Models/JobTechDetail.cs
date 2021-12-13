using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class JobTechDetail
    {
        public long Code { get; set; }
        public String EmployeeFullName { get; set; }
        public String Comment { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        
    }
}