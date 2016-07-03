using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Helpers
{
    public static class GitCommandsHelper
    {
         public class GitCommandResult
         {
             public string OutPut { get; set; }
             public string Error { get; set; }
             public bool isSuccessfull { get; set; }
         }
    }
}