using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using WebhookTest.Helpers;
namespace WebhookTest.GitCommand
{
    public class GitCommands
    {
        public static object GitPull()
        {
            string output = string.Empty;
            string error = string.Empty;
            string _fileName = Path.GetFileName(AppSettings.gitpullcommandPath);
            string _folderPath = AppSettings.gitpullcommandPath.Replace(_fileName,"");

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WorkingDirectory = _folderPath;
            psi.FileName = @"D:\Projects\WebhookTest\WebhookTest\bin\"+ _fileName;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            psi.UseShellExecute = false;
            System.Diagnostics.Process reg;
            reg = System.Diagnostics.Process.Start(psi);

            using (System.IO.StreamReader myOutput = reg.StandardOutput)
            {
                output = myOutput.ReadToEnd();
            }
            using (System.IO.StreamReader myError = reg.StandardError)
            {
                error = myError.ReadToEnd();
            }
            return null;
        }
    }
}