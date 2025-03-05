using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CircleApp.Data.Models;
using System.Threading.Tasks;

namespace CircleApp.Data.Helpers
{
    public class HashtagHelper
    {
        public static List<string> GetHashTag( string postContent)
        {
            var matches = Regex.Matches(postContent, @"#\w+");


            var hashtags = matches.Select(m => m.Value.Replace(@"[^a-z0-9#]", "").ToLower()).Distinct().ToList();

            return hashtags;
        }
       
    }
}
