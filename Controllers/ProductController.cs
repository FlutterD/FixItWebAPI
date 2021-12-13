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
    [RoutePrefix("api/product")]

    public class ProductController : ApiController
    {
        private ProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }
        [Route("")]
        public Product[] Get()
        {
            return productRepository.GetAllProducts();
        }
        //[Route("{employeeCode:int}")]
        //public JobTech[] GetbyEmployee(int employeeCode)
        //{
        //    return jobTechRepository.GetAllJobTechsbyEmployee(employeeCode);
        //}

        //[Route("today/{employeeCode:int}")]
        //public JobTech[] GetTodayJobsByEmployee(int employeeCode)
        //{
        //    return jobTechRepository.GetAllTodayJobTechsbyEmployee(employeeCode);
        //}
    }
}
