using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Helpers
{
    public static class AppSettings
    {
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
    }
}