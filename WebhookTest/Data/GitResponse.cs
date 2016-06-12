using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookTest.Data
{
    public class GitResponse
    {
        public string ref2 { get; set; }
        public string before { get; set; }
        public string after { get; set; }

        public List<GitResponseCommits> Commits { get; set; }

    }

    public class GitResponseCommits
    {
        public string id { get; set; }
        public List<string> added { get; set; }
        public List<string> modified { get; set; }
        public List<string> removed { get; set; }

    }
}