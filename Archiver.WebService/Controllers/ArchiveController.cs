using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Archiver.WebService.Processors;

namespace Archiver.WebService.Controllers
{
    public class ArchiveController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            var startTime = DateTime.Now;
            string fileName = "";
            IHttpActionResult result;
            bool operationStatus;

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.Contents)
                {
                    fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();

                    ArchiveProcessor.CreateTempArchiveFolder();

                    byte[] archivedBytes = ArchiveProcessor.ArchiveBytes(fileName, buffer);

                    ArchiveProcessor.SaveArchiveToDisk(archivedBytes);
                }

                operationStatus = true;
                result = Ok();
            }
            catch (Exception ex)
            {
                operationStatus = false;
                result = InternalServerError(ex);
            }

            var operationDuration = DateTime.Now - startTime;
            ArchiveProcessor.SaveArchiveOperationToDb(fileName, startTime, operationDuration, operationStatus);
            return result;
        }

        [HttpGet]
        public HttpResponseMessage Download()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(ArchiveProcessor.GetArchive());
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "archive.zip"
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
