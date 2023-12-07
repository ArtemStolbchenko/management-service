using management_service.Communication;
using management_service.Model;
using management_service.Repository;
using management_service.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json;

namespace management_service.Controllers
{
    [ApiController]
    [Route("[controller]")] // api example: /Story/test
    public class StoryController : ControllerBase
    {//terrible code, but ah, too little time
        [HttpGet("test")]
        public ActionResult<Story> GetTheThing()
        {//to be removed later on
            Story story = new Story();
            story.Title = "A test story";
            story.Contents.Add(new TextItem(0, "text", "blah blah blah, many times blah"));
            story.Contents.Add(new Media(1, "media", "https://ih1.redbubble.net/image.3137152929.9155/st,small,507x507-pad,600x600,f8f8f8.jpg"));

            StoryRepository.AddStory(story);

            return Ok(story);
        }
        [HttpGet("All")]
        public ActionResult<List<Story>> GetAll() 
        {
            return Ok(StoryRepository.GetStories());
        }
        [HttpPost("Create")]
        public ActionResult<Story> CreateStory([FromBody] JsonElement rawJson)
        {
            try
            {//the following converting shenanigans make the Contents within the Story be deserialized properly
                Story newStory = DeserealizeStory(System.Text.Json.JsonSerializer.Serialize(rawJson));

                if (newStory.Title == string.Empty)
                    return BadRequest("A without a title was given!");

                StoryRepository.AddStory(newStory);

                return Ok(newStory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update")]
        public ActionResult<Story> UpdateStory([FromBody] JsonElement rawJson)
        {
            try
            {
                Story newStory = DeserealizeStory(System.Text.Json.JsonSerializer.Serialize(rawJson));

                if (newStory.Title == string.Empty)
                    return BadRequest("A without a title was given!");

                StoryRepository.UpdateStory(newStory.Id, newStory);

                return Ok(newStory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Publish")]
        public ActionResult PublishStory(int Id)
        {
            Story story;
            try
            {
                story = StoryRepository.GetStories().Where(t => t.Id == Id).First();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Failed to find the given story");
            }
            Publisher.Publish(story);
            return Ok();
        }
        [HttpDelete("Remove")]
        public ActionResult DeleteStory(int Id)
        {
            try
            {
                StoryRepository.DeleteStory(Id);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Failed to find the given story");
            }
            return Ok();
        }
        public override OkObjectResult Ok(object? data)
        {//A more reliable way of converting to JSON
            JsonDocument objectJson = JsonDocument.Parse(JsonConvert.SerializeObject(data).ToString());
            return base.Ok(objectJson);
        }
        private Story DeserealizeStory(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            settings.Converters.Add(new ContentItemConverter());
            Story story = JsonConvert.DeserializeObject<Story>(json, settings) ?? new Story();
            return story;
        }
    }
}