using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fixitupWebAPI.Models
{
    public class Employee
    {
        string fullName = "";
        string code = "";
        string mobileNumber = "";
        string status = "";

        JobTech[] jobTechs ;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        //public JobTech[] JobTechs { get; set; }
    }
}