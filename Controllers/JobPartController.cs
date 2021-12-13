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
    [RoutePrefix("api/jobpart")]
    public class JobPartController : ApiController
    {
        private JobPartRepository jobPartRepository;
        public JobPartController()
        {
            jobPartRepository = new JobPartRepository();
        }
        [Route("")]
        public JobPart[] Get()
        {
            return jobPartRepository.GetAllJobParts();
        }
        [Route("jobCode/{jobCode}")]
        public JobPart[] GetAllJobPartbyJobCode(string jobCode)
        {
            return jobPartRepository.GetAllJobPartsByJobCode(jobCode);
        }
        [Route("exceptreturned/jobCode/{jobCode}")]
        public JobPart[] GetAllJobPartExceptReturnedByJobCode(string jobCode)
        {
            return jobPartRepository.GetJobPartsExceptReturnedByJobCode(jobCode);
        }
    }
}
