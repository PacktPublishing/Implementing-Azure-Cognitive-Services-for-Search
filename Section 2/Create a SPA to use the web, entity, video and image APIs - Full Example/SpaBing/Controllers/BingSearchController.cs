using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Search.EntitySearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.VideoSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using SpaBing.Models;

namespace SpaBing.Controllers
{
    [Route("api/[controller]")]
    public class BingSearchController : Controller
    {
        private readonly string ApiKey = "<enter your API key here>";

        private readonly WebSearchClient webSearchClient;
        private readonly EntitySearchClient entitySearchClient;
        private readonly ImageSearchClient imageSearchClient;
        private readonly VideoSearchClient videoSearchClient;

        public BingSearchController()
        {
            webSearchClient = new WebSearchClient(new Microsoft.Azure.CognitiveServices.Search.EntitySearch.ApiKeyServiceClientCredentials(ApiKey));
            entitySearchClient = new EntitySearchClient(new Microsoft.Azure.CognitiveServices.Search.EntitySearch.ApiKeyServiceClientCredentials(ApiKey));
            imageSearchClient = new ImageSearchClient(new Microsoft.Azure.CognitiveServices.Search.EntitySearch.ApiKeyServiceClientCredentials(ApiKey));
            videoSearchClient = new VideoSearchClient(new Microsoft.Azure.CognitiveServices.Search.EntitySearch.ApiKeyServiceClientCredentials(ApiKey));
        }

        [HttpPost("[action]")]
        public IEnumerable<WebSearchResult> WebSearch([FromBody] WebSearchQuery query)
        {
            var webData = webSearchClient.Web.SearchAsync(query: query.Query, market: query.Market, freshness: query.Freshness).Result;

            if (webData?.WebPages?.Value?.Count == 0)
            {
                // No results
                yield return new WebSearchResult()
                {
                    Name = "No results found"
                };
            }

            foreach(var page in webData.WebPages.Value)
            {
                yield return new WebSearchResult()
                {
                    Name = page.Name,
                    Description = page.Description,
                    DisplayUrl = page.DisplayUrl,
                    Snippet = page.Snippet
                };
            }
        }

        [HttpPost("[action]")]
        public IEnumerable<ImageSearchResult> ImageSearch([FromBody] WebSearchQuery query)
        {
            var imageData = imageSearchClient.Images.SearchAsync(query: query.Query, market: query.Market, freshness: query.Freshness).Result;

            if (imageData?.Value?.Count == 0)
            {
                // No results
                yield return new ImageSearchResult()
                {
                    Name = "No results found"
                };
            }

            foreach (var image in imageData.Value)
            {
                yield return new ImageSearchResult()
                {
                    Name = image.Name,
                    Description = image.Description,
                    ContentUrl = image.ContentUrl,
                    HostPageUrl = image.HostPageUrl,
                    ThumbnailUrl = image.ThumbnailUrl
                };
            }
        }

        [HttpPost("[action]")]
        public IEnumerable<VideoSearchResult> VideoSearch([FromBody] WebSearchQuery query)
        {
            var videoData = videoSearchClient.Videos.SearchAsync(query: query.Query, market: query.Market).Result;

            if (videoData?.Value?.Count == 0)
            {
                // No results
                yield return new VideoSearchResult()
                {
                    Name = "No results found"
                };
            }

            foreach (var video in videoData.Value)
            {
                yield return new VideoSearchResult()
                {
                    Name = video.Name,
                    Description = video.Description,
                    ContentUrl = video.ContentUrl,
                    ThumbnailUrl = video.ThumbnailUrl
                };
            }
        }

        [HttpPost("[action]")]
        public IEnumerable<EntitySearchResult> EntitySearch([FromBody] WebSearchQuery query)
        {
            var entityData = entitySearchClient.Entities.SearchAsync(query: query.Query, market: query.Market).Result;

            if (entityData == null)
            {
                // No results
                yield return new EntitySearchResult()
                {
                    Name = "No results found"
                };
            }

            if (entityData.Entities != null)
            {
                foreach (var entity in entityData.Entities.Value)
                {
                    yield return new EntitySearchResult()
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Url = entity.Url,
                        Types = string.Join(',', entity.EntityPresentationInfo.EntityTypeHints)
                    };
                }
            }
            if(entityData.Places != null)
            {
                foreach (var place in entityData.Places.Value)
                {
                    yield return new EntitySearchResult()
                    {
                        Name = place.Name,
                        Description = place.Description,
                        Url = place.Url,
                        Types = string.Join(',', place.EntityPresentationInfo.EntityTypeHints)
                    };
                }
            }
        }
    }
}
