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
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private CompanyRepository companyRepository;
        public CompanyController()
        {
            companyRepository = new CompanyRepository();
        }
        [Route("")]
        public Company Get()
        {
            return companyRepository.GetComapanyDetails();
        }

    }
}
