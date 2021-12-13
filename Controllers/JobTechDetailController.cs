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
    [RoutePrefix("api/jobtechdetail")]
    public class JobTechDetailController : ApiController
    {
        private JobTechDetailRepository jobTechDetailRepository;

        public JobTechDetailController()
        {
            jobTechDetailRepository = new JobTechDetailRepository();
        }
        [Route("")]
        public JobTechDetail[] Get()
        {
            return jobTechDetailRepository.GetAllJobTechDetails();
        }
        [Route("jobTechCode/{jobTechCode}")]
        public JobTechDetail[] GetAllJobTechDetailbyJobTechCode(long jobTechCode)
        {
            return jobTechDetailRepository.GetAllJobTechDetailsByJobTechCode(jobTechCode);
        }
    }
}
