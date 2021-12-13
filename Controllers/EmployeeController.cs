using fixitupWebAPI.Models;
using fixitupWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fixitupWebAPI.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeRepository employeeRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }
        [Route("")]
        public Employee[] Get()
        {
            return employeeRepository.GetAllEmployees();
        }

        [Route("{code:int}")]
        public Employee GetByCode(int code)
        {
            return employeeRepository.GetEmployeeByCode(code);
        }
    }
}
