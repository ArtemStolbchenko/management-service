using Newtonsoft.Json;

namespace management_service.Model
{
    public class Media : ContentItem
    {
        [JsonProperty("mediaUrl")]
        public string MediaURL { get; set; }
        public Media() : base() 
        {
            this.MediaURL = string.Empty;
        }
        public Media(int id, string name, string MediaURL, string type = "media") : base(id, name, type)
        {
            this.MediaURL = MediaURL;
        }
        public override string ToString()
        {
            return $"`{Name}` - URL: {MediaURL}";
        }
    }
}
