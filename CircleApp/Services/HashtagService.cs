using CircleApp.Data;
using CircleApp.Data.Models;
using System.Text.RegularExpressions;
using CircleApp.Data.Helpers;

namespace CircleApp.Services
{
    public class HashtagService : IHashtagService
    {
        private readonly ApplicationDbContext _context;
        public HashtagService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void ProcessHashtagsForNewPost(string content)
        {
            var saveTagsName = _context.Hashtags.Select(n => n.Name).ToList();
            var hashTags = HashtagHelper.GetHashTag(content);
            hashTags.ForEach(tag =>
            {
                if (saveTagsName.Contains(tag))
                {
                    var hashTag = _context.Hashtags.Where(n => n.Name == tag).First();
                    hashTag.Count += 1;
                    hashTag.UpdatedAt = DateTime.UtcNow;
                    _context.Hashtags.Update(hashTag);
                    _context.SaveChanges();
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

        public void ProcessHashtagsForRemovePost(string content)
        {
            var hashTags=HashtagHelper.GetHashTag(content);
            foreach (var tag in hashTags)
            {
                var saveTag = _context.Hashtags.FirstOrDefault(t => t.Name == tag);
                if(saveTag!=null)
                {
                    saveTag.Count -= 1;
                    saveTag.UpdatedAt = DateTime.UtcNow;
                    _context.Hashtags.Update(saveTag);
                    _context.SaveChanges();
                }

            }
        }
    }
}
