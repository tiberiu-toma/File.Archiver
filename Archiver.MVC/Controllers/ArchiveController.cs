using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Archiver.MVC.Controllers
{
    public class ArchiveController : Controller
    {
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ArchiveFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ArchiveFile(HttpPostedFileBase file)
        {
            try
            {
                string baseAddress = "http://localhost:8081/";
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);


                    using (var httpClient = new HttpClient())
                    {
                        var requestContent = new MultipartFormDataContent();
                        var fileContent = new StreamContent(file.InputStream);
                        requestContent.Add(fileContent, file.FileName, file.FileName);

                        var response = await httpClient.PostAsync(baseAddress + "api/archive", requestContent);

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception();
                        }

                    }
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("DownloadArchive");
            }
            catch
            {
                ViewBag.Message = "File archivation failed.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DownloadArchive()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DownloadArchive(HttpPostedFileBase file = null)
        {
            try
            {
                HttpResponseMessage response = null;
                string baseAddress = "http://localhost:8081/";
                using (var httpClient = new HttpClient())
                {
                    response = await httpClient.GetAsync(baseAddress + "api/archive");
                }

                return File(response.Content.ReadAsStreamAsync().Result, "application/zip");
            }
            catch
            {
                ViewBag.Message = "Download failed. Try again later.";
                return View();
            }
        }
    }
}