using CircleApp.Data;

namespace CircleApp.Services
{
    public interface IHashtagService
    {
        void ProcessHashtagsForNewPost(string content);
        void ProcessHashtagsForRemovePost(string content);
    }
}
