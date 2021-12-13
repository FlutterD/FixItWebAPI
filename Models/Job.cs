using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Job
    {
        public String Code { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public String SerialNumber { get; set; }
        public String Comment { get; set; }
        public String WarrentyF { get; set; }

        public String WarrentyNumber { get; set; }
        public DateTime WarrentyDate { get; set; }
        public DateTime WarrentyEndDate { get; set; }


        public int Status { get; set; }

        public Customer Customer { get; set; }
        public PartNeeded[] PartsNeeded { get; set; }
        public JobTech[] JobTechs { get; set; }
    }
}