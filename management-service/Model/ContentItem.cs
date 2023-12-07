using System.Text.Json.Serialization;

namespace management_service.Model
{
    public abstract class ContentItem
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }

        public ContentItem()
        {
            this.Name = string.Empty;
        }
        public ContentItem(int id, string name, string type)
        {
            this.Id = id;
            Name = name;
            Type = type;
        }
    }
}
