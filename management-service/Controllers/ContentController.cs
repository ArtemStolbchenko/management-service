using Microsoft.AspNetCore.Mvc;
using management_service.Communication;
using management_service.Model;
using management_service.Repository;
using management_service.Utilities;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace management_service.Controllers
{
    [ApiController]
    [Route("Story/Content/")] // api example: /Story/Content/Add
    public class ContentController : ControllerBase
    {
        [HttpPost("Add")]
        public ActionResult<Story> AddContent(int storyId, [FromBody] JsonElement rawJson)
        {
            try
            {
                ContentItem content = DeserializeContent(System.Text.Json.JsonSerializer.Serialize(rawJson));

                Story story = StoryRepository.GetStory(storyId);

                if (story.Title == string.Empty) return BadRequest($"Could not find a story with id {storyId}");
                if (content == null) return BadRequest("No content provided!");

                content.Id = story.Contents.Count;
                story.Contents.Add(content);
                StoryRepository.ReplaceStory(storyId, story);
                return Ok(story);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Remove")]
        public ActionResult<Story> RemoveContent(int StoryId, int ContentId)
        {
            try
            {
                Story story = StoryRepository.GetStory(StoryId);
                story.Contents.RemoveAll(x => x.Id == ContentId);
                StoryRepository.ReplaceStory(story.Id, story);
                return Ok(story);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update")]
        public ActionResult<Story> UpdateContent(int StoryId, [FromBody] JsonElement rawJson)
        {
            try
            {
                ContentItem content = DeserializeContent(System.Text.Json.JsonSerializer.Serialize(rawJson));

                Story story = StoryRepository.GetStory(StoryId);
                int contentId = story.Contents.FindIndex(x => x.Id == content.Id);
                story.Contents[contentId] = content;

                StoryRepository.ReplaceStory(story.Id, story);
                return Ok(story);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public override OkObjectResult Ok(object? data)
        {//A more reliable way of converting to JSON
            JsonDocument objectJson = JsonDocument.Parse(JsonConvert.SerializeObject(data).ToString());
            return base.Ok(objectJson);
        }
        private ContentItem DeserializeContent(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            settings.Converters.Add(new ContentItemConverter());
            ContentItem Content = JsonConvert.DeserializeObject<ContentItem>(json, settings) ?? default;
            return Content;
        }
    }
}
