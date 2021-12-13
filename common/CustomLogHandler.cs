using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FixItWebAPI.common
{

    public class CustomeLogRequestFilter : ActionFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //LogResponseMetadata responseMeta = BuildResponseMetadata(actionExecutedContext.Response);
            //logger.Info(responseMeta.ToString());
            logger.Info(actionExecutedContext.Response.ToString);

            base.OnActionExecuted(actionExecutedContext);

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //LogRequestMetadata requestMeta = BuildRequestMetadata(actionContext.Request);
            //logger.Info(requestMeta.ToString());
            logger.Info(actionContext.Request.ToString);

            base.OnActionExecuting(actionContext);
        }
        private LogResponseMetadata BuildResponseMetadata(HttpResponseMessage response)
        {
            LogResponseMetadata logMetadata = new LogResponseMetadata(response);
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
        private LogRequestMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            LogRequestMetadata logMetadata = new LogRequestMetadata();
            logMetadata.RequestMethod = request.Method.Method;
            logMetadata.RequestUri = request.RequestUri.ToString();
            logMetadata.RequestTimestamp = DateTime.Now;
            logMetadata.RequestContentType = request.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
    }
    public class CustomLogHandler : DelegatingHandler
    {
        //Logger logger = LogManager.GetCurrentClassLogger();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {


            var logMetadata = BuildRequestMetadata(request);

            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = BuildResponseMetadata(logMetadata, response);
            await SendToLog(logMetadata);
            return response;
        }

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            LogMetadata log = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
            return log;
        }
        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            //logger.Info(logMetadata.RequestUri);

            // TODO: Write code here to store the logMetadata instance to a pre-configured log store...
            return true;
        }
    }
}