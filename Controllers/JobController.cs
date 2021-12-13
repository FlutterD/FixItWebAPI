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
    public class JobController : ApiController
    {
        private JobRepository jobRepository;
        public JobController()
        {
            jobRepository = new JobRepository();
        }
        public Job[] Get()
        {
            return jobRepository.GetAllJobs();
        }
        public Job[] GetJobStatus()
        {
            return jobRepository.GetAllJobs();
        }
    }
}
