using System.Web.Mvc;

namespace Archiver.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ArchiveFile", "Archive");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpGet]
        //public ActionResult ArchiveFile()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> ArchiveFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        string baseAddress = "http://localhost:8081/";
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);


        //            using (var httpClient = new HttpClient())
        //            {
        //                var requestContent = new MultipartFormDataContent();
        //                var fileContent = new StreamContent(file.InputStream);
        //                requestContent.Add(fileContent, file.FileName, file.FileName);

        //                var response = await httpClient.PostAsync(baseAddress + "api/archive", requestContent);
        //            }
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return RedirectToAction("DownloadArchive");
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public ActionResult DownloadArchive()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> DownloadArchive(HttpPostedFileBase file = null)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = null;
        //        string baseAddress = "http://localhost:8081/";
        //        using (var httpClient = new HttpClient())
        //        {
        //            response = await httpClient.GetAsync(baseAddress + "api/archive");
        //        }

        //        return File(response.Content.ReadAsStreamAsync().Result, "application/zip");
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}
    }
}