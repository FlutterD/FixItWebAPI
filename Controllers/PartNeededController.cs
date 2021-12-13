using FixItWebAPI.Models;
using FixItWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FixItWebAPI.Controllers
{
    [RoutePrefix("api/partNeeded")]

    public class PartNeededController : ApiController
    {
        private PartNeededRepository partNeededRepository;
        public PartNeededController()
        {
            partNeededRepository = new PartNeededRepository();
        }
        public PartNeeded[] Get()
        {
            return partNeededRepository.GetAllPartsNeeded();
        }
        [Route("jobcode/{code}")]
        public PartNeeded[] GetPartsNeededByJobCode(string code)
        {
            return partNeededRepository.GetAllPartsNeededByJobCode(code);
        }
    }
}
