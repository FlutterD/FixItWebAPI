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
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
   

            private PaymentRepository paymentRepository;
            public PaymentController()
            {
                //visitRepository = new VisitRepository();
            }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Payment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                paymentRepository.SavePayment(payment);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return Ok();
        }

    }
}
