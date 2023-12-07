using management_service.Model;
using System.Runtime.CompilerServices;

namespace management_service.Repository
{
    public static class StoryRepository
    {
        //this is just a mock repository
        //to be replaced with an actual one later
        private static List<Story> _stories= new List<Story>();
        public static void AddStory(Story story)
        {
            story.Id = _stories.Count;
            _stories.Add(story);
        }
        public static List<Story> GetStories()
        {
            return new List<Story>(_stories);
        }
        public static void ReplaceStory(int Id, Story newStory)
        {
            try
            {
                _stories[Id] = newStory;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to update a story with id {Id}. {e.Message}");
            }
        }
        public static void UpdateStory(int Id, Story newStory)
        {
            try
            {
                _stories[Id].Title = newStory.Title;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to update a story with id {Id}. {e.Message}");
            }
        }
        public static Story GetStory(int Id)
        {
            try
            {
                return _stories[Id];
            } catch (Exception e)
            {
                Console.WriteLine($"Failed to get a story with id {Id}. {e.Message}");
                return default;
            }
        }
        public static void DeleteStory(int Id)
        {
            try
            {
                _stories.RemoveAt(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to update a story with id {Id}. {e.Message}");
            }
        }
    }
}
