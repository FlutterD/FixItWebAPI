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
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private CustomerRepository customerRepository;
        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }
        public Customer[] Get()
        {
            return customerRepository.GetAllCustomers();
        }
        [Route("{code:int}")]
        public Customer GetCustomerByCode(int code)
        {
            return customerRepository.GetCustomerByCode(code);
        }
    }
}
