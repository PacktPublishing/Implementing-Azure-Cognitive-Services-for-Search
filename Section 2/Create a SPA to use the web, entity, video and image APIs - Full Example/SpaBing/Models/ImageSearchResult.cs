namespace SpaBing.Models
{
    public class ImageSearchResult : ISearchResult
    {
        public string Name { get; set; }
        public string ContentUrl { get; set; }
        public string HostPageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
    }
}
