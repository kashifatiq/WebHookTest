using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Helpers
{
    public static class AppSettings
    {
        /// <summary>
        /// fit pull command name with folder path
        /// </summary>
        public static string gitpullcommandPath { get { return readWebConfig("gitpullcommandPath"); } }
        
        /// <summary>
        /// folder path where commands will actually run
        /// </summary>
        public static string repoToPullFolerPath { get { return readWebConfig("repoToPullFolerPath"); } }

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