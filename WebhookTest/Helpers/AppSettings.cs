using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Helpers
{
    public static class AppSettings
    {
        /// <summary>
        /// start / stop start or stop this app completly
        /// </summary>
        public static bool serviceStatus
        {
            get
            {
                string status = readWebConfig("serviceStatus");
                return status == "" ? false : Convert.ToBoolean(status);
            }
        }

        /// <summary>
        /// git pull command name with folder path
        /// </summary>
        public static string gitpullcommandPath { get { return readWebConfig("gitpullcommandPath"); } }
        
        /// <summary>
        /// folder path where commands will actually run
        /// </summary>
        public static string repoToPullFolerPath { get { return readWebConfig("repoToPullFolerPath"); } }

        /// <summary>
        /// folder path where files will copied and pushed to repo
        /// </summary>
        public static string BackupFolderPath { get { return readWebConfig("BackupFolderPath"); } }

        /// <summary>
        /// git push command name with folder path
        /// </summary>
        public static string gitpushcommandPath { get { return readWebConfig("gitpushcommandPath"); } }

        private static string readWebConfig(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            if(value != null)
            {
                return value.ToString();
            }
            return "";
        }

        public static Dictionary<string, string> VerifyProperties()
        {
            Dictionary<string, string> dicProperties = new Dictionary<string, string>();
            dicProperties.Add("Service Status", AppSettings.serviceStatus == true ? "On" : "Stoped");
            dicProperties.Add("Files to ignore", "");
            dicProperties.Add("Folders to ignore", "");
            dicProperties.Add("Desired file extentions", "");
            dicProperties.Add("Backup folder path", AppSettings.BackupFolderPath);
            dicProperties.Add("Repo folder path", AppSettings.repoToPullFolerPath);
            dicProperties.Add("Git pull batch file path", AppSettings.gitpullcommandPath);
            dicProperties.Add("Git push batch file path", AppSettings.gitpushcommandPath);
            return dicProperties;
        }
    }
}