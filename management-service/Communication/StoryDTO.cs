using management_service.Model;

namespace management_service.Communication
{
    public class StoryDTO
    {
        public int? ArticleId { get; set; }
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public string PublishLink { get; set; }

        /// <summary>
        /// In the database this is a table of mediaIds
        /// </summary>
        public List<ContentItem> Content { get; set; }

        public StoryDTO(int? articleId = null, DateTime? publishDate = null, string publishLink = "", List<ContentItem> content = null, string name = "")
        {
            this.ArticleId = articleId;
            this.Name = name;
            this.PublishDate = (DateTime)((publishDate == null) ? DateTime.Now : publishDate);
            this.PublishLink = publishLink;
            this.Content = content;
        }
        public StoryDTO(Story story)
        {
            this.ArticleId = story.Id;
            this.Name = story.Title;
            this.PublishDate = DateTime.Now;
            this.PublishLink = "";
            this.Content = story.Contents;
        }
    }
}
