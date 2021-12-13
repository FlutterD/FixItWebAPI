using FixItWebAPI.Models;
using FixItWebAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FixItWebAPI.Controllers
{
    [RoutePrefix("api/visit")]
    public class VisitController : ApiController
    {

        private VisitRepository visitRepository;
        public VisitController()
        {
            visitRepository = new VisitRepository();
        }
        public Visit[] Get()
        {
            return visitRepository.GetAllVisits();
        }
        [Route("{visitCode:int}")]
        public Visit GetVisitByCode(long visitCode)
        {
            return visitRepository.GetVisitByCode(visitCode);
        }
        [Route("jobtech/{jobTechCode:int}")]
        public Visit[] GetVisitByJobTechCode(long jobTechCode)
        {
            return visitRepository.GetVisitByJobTechCode(jobTechCode);
        }

        [Route("employee/{employee:int}")]
        public Visit[] GetVisitByEmployeeCode(int employee)
        {
            return visitRepository.GetAllVisitsByEmployee(employee);
        }
        [Route("employeeall/{employee:int}")]
        public Visit[] GetVisitByEmployeeCodeAll(int employee)
        {
            return visitRepository.GetAllVisitsByEmployee_old(employee);
        }






        //[Route("add")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]TechVisitDetail visit)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                visitRepository.SaveTechnicianVisit(visit);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpPut]
        [Route("partsissued")]
        public IHttpActionResult PutJobParts([FromBody]List<JobPart> issuedParts)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                visitRepository.SaveTechnicianVisitJobParts(issuedParts);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        [Route("partsneeded")]
        public IHttpActionResult PutNeededParts([FromBody]List<JobPart> neededParts)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                visitRepository.SaveTechnicianVisitJobNeededParts(neededParts);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("location")]
        public IHttpActionResult PutCustomerLocation([FromBody]Location customerLocation)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                visitRepository.SaveCustomerLocation(customerLocation);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        [Route("payment")]
        public IHttpActionResult PutCustomerReceipt([FromBody]Payment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                visitRepository.SaveCustomerReceipt(payment);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //[HttpPut]
        //[Route("image/{filename}/{filetype}")]
        //public HttpResponseMessage PutImage([FromUri]string filename, [FromUri]string filetype)
        //{

        //    var task = this.Request.Content.ReadAsStreamAsync();
        //    task.Wait();
        //    Stream requestStream = task.Result;
        //    try
        //    {
        //        Stream fileStream = File.Create(HttpContext.Current.Server.MapPath("~/JobPhotos/" + filename+"."+ filetype));
        //        requestStream.CopyTo(fileStream);
        //        fileStream.Close();
        //        requestStream.Close();
        //    }
        //    catch (IOException)
        //    {
        //        throw new HttpResponseException( HttpStatusCode.InternalServerError);
        //    }

        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response.StatusCode = HttpStatusCode.Created;
        //    return response;
        //}

        [HttpPost]
        [Route("uploadfile")]
        public IHttpActionResult UploadFile()
        {

            HttpPostedFile file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    String path = Path.Combine(
                        HttpContext.Current.Server.MapPath("~/JobPhotos"),
                        fileName
                    );

                    file.SaveAs(path);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
             return Ok( file != null ? "/JobPhotos/" + file.FileName : null);
           // return file != null ? strStaticPath +"//"+ file.FileName : null;
        }

        //[HttpPost]
        //[Route("uploadAsync")]
        //public async System.Threading.Tasks.Task<IHttpActionResult>
        //    UploadAsync(System.Threading.CancellationToken cancellationToken)
        //{
        //    if (!Request.HasFormContentType)
        //        return BadRequest();

        //    var form = Request.Form;
        //    foreach (var formFile in form.Files)
        //    {
        //        using (var readStream = formFile.OpenReadStream())
        //        {
        //            // Do something with the uploaded file
        //        }
        //    }


        //    return Ok();
        //}
    }
    
}
