using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;

namespace FixItWebAPI
{
    public class CustomFormatter : MediaTypeFormatter
    {
        public CustomFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }


        public override bool CanReadType(Type type)
        {
            return type == typeof(Attachment);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        //public async override Readfrom
        public async override
            System.Threading.Tasks.Task<object> 
            ReadFromStreamAsync(Type type, System.IO.Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var provider = await content.ReadAsMultipartAsync();

            var modelContent = provider.Contents
                .FirstOrDefault(c => c.Headers.ContentType.MediaType == "application/json"); // Can also use ContentDisposition.Name.Normalize == "attachment"

            var attachment = await modelContent.ReadAsAsync<System.Net.Mail.Attachment>();

            var fileContents = provider.Contents
                .Where(c => c.Headers.ContentType.MediaType == "image/jpeg").FirstOrDefault(); // can also use ContentDisposition.Name.Normalize() == "image"

          //  attachment.Content = await fileContents.ReadAsByteArrayAsync();

            return attachment;

        }
    }
}