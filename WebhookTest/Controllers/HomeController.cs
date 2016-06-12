using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebhookTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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