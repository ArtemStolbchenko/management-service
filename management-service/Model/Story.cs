using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace management_service.Model
{
    public class Story
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("contents")]
        public List<ContentItem> Contents { get; set; }
        public Story() 
        {
            this.Title = string.Empty;
            this.Contents = new List<ContentItem>();
        }
    }
}
