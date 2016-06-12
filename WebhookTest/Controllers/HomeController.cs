using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebhookTest.Helpers;
namespace WebhookTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void TestJson()
        {
            JsonHandler jHandler = new JsonHandler();
            string fileNamePath = Server.MapPath("../Data/SampleGitResponse.json");
            List<string> lstPushedFiles = new List<string>();
            using (StreamReader r = new StreamReader(fileNamePath))
            {
                lstPushedFiles = jHandler.GetDesirePushedFiles(r.ReadToEnd());
            }
            
        }

        [HttpPost]
        public ActionResult Webhook()
        {
            var json = "";
            using (var inputStream = new System.IO.StreamReader(Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }
            return Json(new {message = "kashif",jsonData = json});
        }
    }
}