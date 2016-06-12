using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebhookTest.Data;
namespace WebhookTest.Helpers
{
    public class JsonHandler
    {
       private List<string> filesToFilter = new List<string>();
       private List<string> foldersToIgnore = new List<string>();
       private List<string> filesToIgnore = new List<string>();

        public List<string> GetDesirePushedFiles(string jSonString)
        {
            filesToFilter = System.Configuration.ConfigurationManager.AppSettings["desiredFiles"].ToString().Split(',').ToList();
            foldersToIgnore = System.Configuration.ConfigurationManager.AppSettings["ignoreFolder"].ToString().Split(',').ToList();
            filesToIgnore = System.Configuration.ConfigurationManager.AppSettings["ignoreFile"].ToString().Split(',').ToList();
    
            List<string> lstDesiredPushedFiles = new List<string>();
            GitResponse _gitResponse = this.ReadJsonString(jSonString);
            foreach(GitResponseCommits commits in _gitResponse.Commits)
            {
               foreach(string commitedFile in commits.added)
               {
                   GetFilteredFiles(lstDesiredPushedFiles, commitedFile);
               }
               foreach (string commitedFile in commits.modified)
               {
                   GetFilteredFiles(lstDesiredPushedFiles, commitedFile);
               }
            }
            return lstDesiredPushedFiles;
        }

        private void GetFilteredFiles(List<string> lstDesiredPushedFiles, string commitedFile)
        {
            string fileExtention = Path.GetExtension(commitedFile);
            string fileName = Path.GetFileName(commitedFile);
            string OnlyFolderPath = commitedFile.Replace(fileName,"");
            bool isFolderToIgnore = false;
            foreach (string strfolderPaths in foldersToIgnore)
            {
                if(OnlyFolderPath.Contains(strfolderPaths))
                {
                    isFolderToIgnore = true;
                }
            }
            if (filesToFilter.Contains(fileExtention) && !filesToIgnore.Contains(fileName) && isFolderToIgnore == false)
            {
                lstDesiredPushedFiles.Add(commitedFile);
            }
        }

        private GitResponse ReadJsonString(string jSon)
        {
            GitResponse items;
            items = JsonConvert.DeserializeObject<GitResponse>(jSon);
            return items;
        }
    }
}