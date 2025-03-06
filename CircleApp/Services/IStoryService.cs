using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IStoryService
    {
       public List<Story> GetAllStories();
       public Story CreateStory(Story story);
    }
}
