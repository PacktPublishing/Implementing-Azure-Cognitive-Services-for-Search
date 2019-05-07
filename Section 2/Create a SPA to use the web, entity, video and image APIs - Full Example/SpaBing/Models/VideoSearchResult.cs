namespace SpaBing.Models
{
    public class VideoSearchResult : ISearchResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
