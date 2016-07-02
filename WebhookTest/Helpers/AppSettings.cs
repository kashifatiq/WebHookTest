using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Helpers
{
    public static class AppSettings
    {
        public static string gitpullcommandPath { get { return readWebConfig("gitpullcommandPath"); } }
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