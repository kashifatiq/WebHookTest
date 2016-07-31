using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace EventLogs
{
    public static class LogsHandler
    {
        private static readonly ILog m_Logger = log4net.LogManager.GetLogger(typeof(LogsHandler).FullName);

        public static void LogTest()
        {
            m_Logger.Debug("Home page viewed.");
            m_Logger.Warn("This is a warning message!");
            m_Logger.Error("Eorr");
        }

        public static void LogException(Exception ex)
        {

        }

        public static void LogInfo(string infoMessage)
        {

        }

        public static void LogWarning(string warningMessage)
        {

        }
    }
}
