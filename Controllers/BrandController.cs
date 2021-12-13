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
    [RoutePrefix("api/brand")]
    public class BrandController : ApiController
    {
        private BrandRepository brandRepository;
        public BrandController()
        {
            brandRepository = new BrandRepository();
        }
        [Route("")]
        public Brand[] Get()
        {
            return brandRepository.GetAllBrands();
        }
        [Route("{code}")]
        public Brand GetBrandByCode(String code)
        {
            return brandRepository.GetBrandByCode(code);
        }
    }
}
