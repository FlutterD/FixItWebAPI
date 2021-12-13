using fixitupWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class JobTech
    {
        long code;
        
        DateTime date = DateTime.MinValue;
        DateTime endDate = DateTime.MinValue;

        public long Code { get; set; }
        public String JobCode { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public Employee Employee { get; set; }
        public JobTechDetail[] JobTechDetails { get; set; }
        public String EmployeeName { get; set; }
        public String Comments { get; set; }

        public Job Job { get; set; }
        //public long EmployeeCode { get; set; }
        //  public Visit[] Visits { get; set; }
    }

}