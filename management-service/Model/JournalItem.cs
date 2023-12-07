using Newtonsoft.Json;

namespace management_service.Model
{
    public class JournalItem : ContentItem
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("attachmentURL")]
        public string AttachmentURL { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
        public JournalItem() : base()
        {
            this.Content = string.Empty;
            this.AttachmentURL = string.Empty;
            this.Source = string.Empty;
        }
        public JournalItem(int id, string name, string type, string content, string attachmentURL, string source) : base(id, name,type)
        {
            Content = content;
            AttachmentURL = attachmentURL;
            Source = source;
        }
        public override string ToString()
        {
            return $"`{Name}`\n{Content}\nPicturURL: {AttachmentURL}\nSource: {Source}";
        }
    }
}
