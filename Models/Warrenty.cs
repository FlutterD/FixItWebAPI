using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Warrenty
    {
        public String SerialNumber { get; set; }
        public String Model { get; set; }
        public String Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}