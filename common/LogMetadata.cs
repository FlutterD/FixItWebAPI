using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace FixItWebAPI.common
{
    public class LogMetadata
    {
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
    }
    public class LogRequestMetadata
    {
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public override string ToString()
        {
        //2020 - 07 - 20 03:35:29.7585 
        //INFO Method: GET, 
        //RequestUri: 'http://localhost/v2/api/visit/employeeall/102/', Version: 1.1, 
        //Content: System.Web.Http.WebHost.HttpControllerHandler + LazyStreamContent,
        //Headers:

            return String.Format("{0}\t{1}\t{2}\t{3}", RequestTimestamp.ToString(), RequestMethod, RequestUri, RequestContentType);
        }
    }
    public class LogResponseMetadata
    {
        private HttpResponseMessage response;

        public LogResponseMetadata(HttpResponseMessage response)
        {
            this.response = response;
            //this.ResponseContent = response.Content.ToStri
            this.ResponseTimestamp = DateTime.Now;
            //this.ResponseContent = response.Content.
        }

        public string ResponseContent { get; set; }
        public string ResponseContentType { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }

        public override string ToString()
        {
        //2020 - 07 - 20 03:35:38.4265 
        //INFO StatusCode: 200, 
        //ReasonPhrase: 'OK', Version: 1.1, 
        //Content: System.Net.Http.ObjectContent`1[[FixItWebAPI.Models.Visit[], FixItWebAPI, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null]],
        //Headers:
            return String.Format("{0}\t{1}\t{2}\t{3}",ResponseTimestamp.ToString(),ResponseStatusCode,ResponseContentType,ResponseContent);
        }
    }
}