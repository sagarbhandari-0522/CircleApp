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
        public static ApplicationDbContext _context;
        public static void Hashtag( ApplicationDbContext context, string postContent)
        {
           _context = context;
            var saveTagsName = _context.Hashtags.Select(n => n.Name).ToList();
            var matches = Regex.Matches(postContent, @"#\w+");


            var hashtags=matches.Select(m=>m.Value.Replace(@"[^a-z0-9#]","").ToLower()).Distinct().ToList();
            hashtags.ForEach(tag =>
            {   
                if(saveTagsName.Contains(tag))
                {
                    var hashTag = _context.Hashtags.Where(n => n.Name == tag).First();
                    hashTag.Count += 1;
                    hashTag.UpdatedAt = DateTime.UtcNow;
                    _context.Hashtags.Update(hashTag);
                }
                else
                {
                    Hashtag hashTag = new Hashtag();
                    hashTag.Name = tag;
                    hashTag.CreatedAt = DateTime.UtcNow;
                    hashTag.UpdatedAt = DateTime.UtcNow;
                    hashTag.Count = 1;
                    _context.Hashtags.Add(hashTag);
                    _context.SaveChanges();
                }
            });
        }
        public static List<string> GetHashTag(ApplicationDbContext context, string postContent)
        {
            var lista = new List<string>() { "Sagar", "samundra" };
            return lista;
        }
    }
}
