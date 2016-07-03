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
            //GitCommand.GitCommands.GitPull();
            TestJson();
            return View();
        }

        public void TestJson()
        {
            try
            {
                JsonHandler jHandler = new JsonHandler();
                string fileNamePath = Server.MapPath("~/Data/SampleGitResponse.json");
                List<string> lstPushedFiles = new List<string>();
                using (StreamReader r = new StreamReader(fileNamePath))
                {
                    lstPushedFiles = jHandler.GetDesirePushedFiles(r.ReadToEnd());
                }
                if (lstPushedFiles.Count > 0)
                {
                    CopyFilesToBackupFolder(lstPushedFiles);
                }
            }
            catch(Exception)
            {
                //TODO log error here
            }
        }

        private void CopyFilesToBackupFolder(List<string> lstPushedFiles)
        {
            try
            {
                GitCommandsHelper.GitCommandResult _gitResult = GitCommand.GitCommands.GitPull(); // run git pull to download all files
                string _repoToPullFolerPath = AppSettings.repoToPullFolerPath;
                if (_gitResult.isSuccessfull == true)
                {
                    #region copy desired files to pre-defined forlder
                    string _destinationPath = string.Empty;
                    foreach (string fileToBackup in lstPushedFiles)
                    {
                        string _fileName = Path.GetFileName(fileToBackup);
                        _destinationPath = Path.Combine(AppSettings.BackupFolderPath, _fileName);
                        try
                        {
                            System.IO.File.Copy(Path.Combine(_repoToPullFolerPath, fileToBackup), _destinationPath, true);
                        }
                        catch (Exception)
                        {
                            //TODO log error here
                        }
                    }
                    #endregion
                }
            }
            catch(Exception)
            {
                //TODO log error here
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
            return Json(new {message = "OK",jsonData = json});
        }
    }
}