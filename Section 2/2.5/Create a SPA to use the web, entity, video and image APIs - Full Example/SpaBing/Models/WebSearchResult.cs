namespace SpaBing.Models
{
    public class WebSearchResult : ISearchResult
    {
        public string Name { get; set; }
        public string DisplayUrl { get; set; }
        public string Snippet { get; set; }
        public string Description { get; set; }
    }

    
}
