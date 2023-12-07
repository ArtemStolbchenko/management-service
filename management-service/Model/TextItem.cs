using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace management_service.Model
{
    public class TextItem : ContentItem
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        public TextItem() : base()
        {
            this.Text = string.Empty;
        }
        public TextItem(int id, string name, string text, string type = "text") : base(id, name, type)
        {
            Text = text;
        }
        public override string ToString()
        {
            return $"`{Name}` : `{Text}` ";
        }
    }
}
