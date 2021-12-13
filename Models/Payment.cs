using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixItWebAPI.Models
{
    public class Payment
    {
        public String JobCode { get; set; }
        public String PaymentDate { get; set; }
        public double PartsValue { get; set; }
        public double LaborValue { get; set; }
        public double OthersValue { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int CustomerCode { get;  set; }
        String customerPhoneNumber{ get;  set; }
        public double partsValue{ get;  set; }
        public double exchangeRate{ get;  set; }

        public DateTime paymentDate{ get;  set; }

        public double laborValue{ get;  set; }

        public double othersValue{ get;  set; }

        public double latitude{ get;  set; }

        public double longitude{ get;  set; }

    }

}