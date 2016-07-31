using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebhookTest.Helpers;
using EventLogs;
using WebhookTest.Models;
namespace WebhookTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModel _model = new IndexViewModel();
            _model.dicProperties = new Dictionary<string, string>();
            _model.dicProperties = AppSettings.VerifyProperties();

            LogsHandler.LogTest();
            //GitCommand.GitCommands.GitPull();
            //TestJson();
            return View(_model);
        }

        private static void VerifyProperties(IndexViewModel _model)
        {
            _model.dicProperties = new Dictionary<string, string>();
            _model.dicProperties.Add("Service Status", AppSettings.serviceStatus == true ? "On" : "Stoped");
            _model.dicProperties.Add("Files to ignore", "");
            _model.dicProperties.Add("Folders to ignore", "");
            _model.dicProperties.Add("Desired file extentions", "");
            _model.dicProperties.Add("Backup folder path", AppSettings.BackupFolderPath);
            _model.dicProperties.Add("Repo folder path", AppSettings.repoToPullFolerPath);

            _model.dicProperties.Add("Git pull batch file path", AppSettings.gitpullcommandPath);
            _model.dicProperties.Add("Git push batch file path", AppSettings.gitpushcommandPath);
        }

        /// <summary>
        /// test pull->copy files->push (not from live hit but from hard coded json string)
        /// </summary>
        public void TestJson()
        {
            try
            {
                if (!AppSettings.serviceStatus)
                    return;
                string fileNamePath = Server.MapPath("~/Data/SampleGitResponse.json");
                List<string> lstPushedFiles = new List<string>();
                StreamReader r = new StreamReader(fileNamePath);
                PullCopyPush(r.ReadToEnd());
            }
            catch(Exception ex)
            {
                //TODO log error here
            }
        }

        /// <summary>
        /// filter out desired files from json string, pull whole repo, copy desired files to hostory folder, push history folder into repo
        /// </summary>
        /// <param name="jSonString"></param>
        private void PullCopyPush(string jSonString)
        {
            List<string> PushedFiles = new List<string>();
            JsonHandler jHandler = new JsonHandler();
            PushedFiles = jHandler.GetDesirePushedFiles(jSonString);
            if (PushedFiles.Count > 0)
            {
                // run git pull to download all files
                // all the errors and files download logs will be handeld in function itself
                GitCommandsHelper.GitCommandResult _gitPullResult = GitCommand.GitCommands.GitPull();
                if (_gitPullResult.isSuccessfull)
                {
                    CopyFilesToBackupFolder(PushedFiles);

                    // run it push to push 
                    // all the errors and files download logs will be handeld in function itself
                    GitCommandsHelper.GitCommandResult _gitGitPushResult = GitCommand.GitCommands.GitPush(); // run git push to push all updates from backup folder
                    if(_gitGitPushResult.isSuccessfull)
                    {
                        //TODO log process completed
                    }
                    else
                    {
                        //TODO log push error
                    }
                }
                else
                {
                    //TODO log that we are not pushing anything becouse pull is failed 
                }
            }
        }

        private void CopyFilesToBackupFolder(List<string> lstPushedFiles)
        {
            try
            {
                string _repoToPullFolerPath = AppSettings.repoToPullFolerPath;
                string _destinationPath = string.Empty;
                if(!Directory.Exists(AppSettings.BackupFolderPath))
                {
                    Directory.CreateDirectory(AppSettings.BackupFolderPath);
                }
                foreach (string fileToBackup in lstPushedFiles)
                {
                    string _fileName = Path.GetFileName(fileToBackup);
                    _destinationPath = Path.Combine(AppSettings.BackupFolderPath, _fileName);
                    try
                    {
                        System.IO.File.Copy(Path.Combine(_repoToPullFolerPath, fileToBackup), _destinationPath, true);
                    }
                    catch (Exception ex)
                    {
                        //TODO log error here
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error here
            }
        }

        /// <summary>
        /// actual method that will be hit by git if anyone pushes something in repo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Webhook()
        {
            if (!AppSettings.serviceStatus)
                return Json(new { message = "OK", jsonData = "" });
            string json = "";
            var inputStream = new System.IO.StreamReader(Request.InputStream);
            json = inputStream.ReadToEnd();
            PullCopyPush(json);
            return Json(new { message = "OK", jsonData = json });
        }
    }
}