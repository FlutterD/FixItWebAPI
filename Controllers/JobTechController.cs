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
    [RoutePrefix("api/jobtech")]
    public class JobTechController : ApiController
    {
        private JobTechRepository jobTechRepository;
  
        public JobTechController()
        {
            jobTechRepository = new JobTechRepository();
        }
        [Route("")]
        public JobTech[] Get()
        {
            return jobTechRepository.GetAllJobTechs();
        }
        [Route("{employeeCode:int}")]
        public JobTech[] GetbyEmployee(int employeeCode)
        {
            return jobTechRepository.GetAllJobTechsbyEmployee(employeeCode);
        }
        
        [Route("today/{employeeCode:int}")]
        public JobTech[] GetTodayJobsByEmployee(int employeeCode)
        {
            return jobTechRepository.GetAllTodayJobTechsbyEmployee(employeeCode);
        }

        [Route("jobcode/{jobCode}")]
        public JobTech[] GetAllTodayJobTechsbyCode(String jobCode)
        {
            return jobTechRepository.GetAllJobTechsbyJobCode(jobCode);
        }

        [Route("jobtechdetails/{jobCode}")]
        public JobTechDetail[] GetAllTodayJobTechDetailssbyJobCode(String jobCode)
        {
            return jobTechRepository.GetAllJobTechDetailsbyJobCode(jobCode);
        }


    }
}
