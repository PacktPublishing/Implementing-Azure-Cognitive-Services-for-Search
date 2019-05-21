namespace SpaBing.Models
{
    public class EntitySearchResult : ISearchResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Types { get; set; }
    }
}
