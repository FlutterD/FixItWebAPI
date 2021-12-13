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
    [RoutePrefix("api/jobstatus")]
    public class JobStatusController : ApiController
    {
        private JobStatusRepository jobStatusRepository;
        public JobStatusController()
        {
            jobStatusRepository = new JobStatusRepository();
        }
        [Route("")]
        public JobStatus[] Get()
        {
            return jobStatusRepository.GetAllJobStauses();
        }
    }
}
