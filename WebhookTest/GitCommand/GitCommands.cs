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
        public static GitCommandsHelper.GitCommandResult GitPull()
        {
            GitCommandsHelper.GitCommandResult _gitResults = new GitCommandsHelper.GitCommandResult();
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = AppSettings.repoToPullFolerPath;
                psi.FileName = AppSettings.gitpullcommandPath;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                psi.UseShellExecute = false;
                System.Diagnostics.Process reg;
                reg = System.Diagnostics.Process.Start(psi);

                using (System.IO.StreamReader myOutput = reg.StandardOutput)
                {
                    //TODO read this output for better logging of whats downloaded
                    _gitResults.OutPut = myOutput.ReadToEnd();
                }
                using (System.IO.StreamReader error = reg.StandardError)
                {
                    //TODO handle any errors from git command which are not exceptions
                    _gitResults.Error = error.ReadToEnd();
                }

                _gitResults.isSuccessfull = true;
                return _gitResults;
            }
            catch(Exception)
            {
                _gitResults.isSuccessfull = false;
                return _gitResults;
            }
        }

        public static GitCommandsHelper.GitCommandResult GitPush()
        {
            GitCommandsHelper.GitCommandResult _gitResults = new GitCommandsHelper.GitCommandResult();
            try
            {

                return _gitResults;
            }
            catch (Exception)
            {
                _gitResults.isSuccessfull = false;
                return _gitResults;
                //TODO log error
            }
        }
    }
}