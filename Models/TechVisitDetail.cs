using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class TechVisitDetail
    {
        public int JTEC_CODE { get; set; }
        public String Comment { get; set; }
        public String JTECD_DATE { get; set; }
        public String JTECD_EDATE { get; set; }
        public String JTECD_TIME { get; set; }
        public int JTECD_STATUS { get; set; }

    }
}