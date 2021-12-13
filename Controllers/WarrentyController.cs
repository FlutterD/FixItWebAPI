using FixItWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FixItWebAPI.Controllers
{
    [RoutePrefix("api/warrenty")]
    public class WarrentyController : ApiController
    {
        private WarrentyRepository warrentyRepository;

        public WarrentyController()
        {
            warrentyRepository = new WarrentyRepository();
        }
        [Route("")]
        public Warrenty[] Get()
        {
            return warrentyRepository.GetAllWarrenties();
        }
        [Route("{serialnumber}")]
        public Warrenty GetWarrentyBySerialnumber(String serialnumber)
        {
            return warrentyRepository.GetWarrenty(serialnumber);
        }
        [Route("{serialNumber}/{modelNumber}")]
        public Warrenty GetWarrenty(String serialNumber,String modelNumber)
        {
            return warrentyRepository.GetWarrenty(serialNumber,modelNumber);
        }
    }
}
