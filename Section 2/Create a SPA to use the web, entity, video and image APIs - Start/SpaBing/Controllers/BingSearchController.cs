using System;
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
        public BingSearchController()
        {
        }

        [HttpPost("[action]")]
        public IEnumerable<WebSearchResult> WebSearch([FromBody] WebSearchQuery query)
        {
            throw new NotImplementedException("Todo");
        }

        [HttpPost("[action]")]
        public IEnumerable<ImageSearchResult> ImageSearch([FromBody] WebSearchQuery query)
        {
            throw new NotImplementedException("Todo");
        }

        [HttpPost("[action]")]
        public IEnumerable<VideoSearchResult> VideoSearch([FromBody] WebSearchQuery query)
        {
            throw new NotImplementedException("Todo");
        }

        [HttpPost("[action]")]
        public IEnumerable<EntitySearchResult> EntitySearch([FromBody] WebSearchQuery query)
        {
            throw new NotImplementedException("Todo");
        }
    }
}
