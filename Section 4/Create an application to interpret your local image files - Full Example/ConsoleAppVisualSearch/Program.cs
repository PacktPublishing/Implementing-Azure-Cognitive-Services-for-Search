using Microsoft.Azure.CognitiveServices.Search.VisualSearch;
using Microsoft.Azure.CognitiveServices.Search.VisualSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ConsoleAppVisualSearch
{
    class Program
    {
        private static readonly string ApiKey = "<enter your S9 key here>";
        private static VisualSearchRequest GetSearchRequest(string url = "", string token = "")
        {
            if (!string.IsNullOrEmpty(url))
            {
                return new VisualSearchRequest
                {
                    ImageInfo = new ImageInfo
                    {
                        Url = url
                    }
                };
            }

            return new VisualSearchRequest
            {
                ImageInfo = new ImageInfo
                {
                    ImageInsightsToken = token
                }
            };
        }
        static void Main(string[] args)
        {
            var client = new VisualSearchClient(new ApiKeyServiceClientCredentials(ApiKey));

            // Optionally you can send the local files:
            //var stream = new FileStream(Path.Combine("TestImages", "armchair1.png"), FileMode.Open);
            //var visualSearchResults = client.Images.VisualSearchMethodAsync(image: stream, market: "en-us", knowledgeRequest: (string)null).Result;

            // Or use a request containing a URL:
            var vsr = GetSearchRequest("https://peterrubiconstorage.blob.core.windows.net/packt/armchair1.png");
            var visualSearchResults = client.Images.VisualSearchMethodAsync(market: "en-us", knowledgeRequest: vsr).Result;

            // Visual Search results
            if (visualSearchResults.Image?.ImageInsightsToken != null)
            {
                Console.WriteLine($"Uploaded image insights token: {visualSearchResults.Image.ImageInsightsToken}");
            }
            else
            {
                Console.WriteLine("Couldn't find image insights token!");
            }

            var listOfDescriptions = new List<string>();

            // Loop through results
            if (visualSearchResults.Tags.Count > 0)
            {
                Console.WriteLine($"Found {visualSearchResults.Tags.Count} visual search tag(s)");
                int i = 0;

                foreach (var tag in visualSearchResults.Tags)
                {
                    if(!string.IsNullOrEmpty(tag.DisplayName))
                    {
                        listOfDescriptions.Add(tag.DisplayName);
                    }
                    Console.WriteLine($"\n\nTag [{++i}]: Name {tag.Name} Description {tag.Description} DisplayName {tag.DisplayName}");

                    Console.WriteLine($"Tag [{i}] action count: {tag.Actions.Count}");

                    if(!tag.Actions.Any())
                    {
                        Console.WriteLine("Couldn't find tag actions!");
                        continue;
                    }

                    foreach (var action in tag.Actions)
                    {
                        Console.WriteLine($"\n-- ActionType {action.ActionType} --");
                        try
                        {
                            // Download all VisualSearch results
                            if (action.ActionType == "VisualSearch")
                            {
                                foreach (ImageObject o in (action as ImageModuleAction).Data.Value)
                                {
                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.DownloadFile(new Uri(o.ContentUrl), $"Output\\{action.ActionType}{o.ImageId}.{o.EncodingFormat}");
                                    }
                                    Console.WriteLine($"Action: ContentURL: {o.ContentUrl} DisplayName {action.DisplayName}");
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Action: DisplayName {action.DisplayName} No ImageObject found");
                        }
                    }
                }
                Console.WriteLine($"\n----------------\nSearch tag descriptions:\n{string.Join(',', listOfDescriptions)}");
            }
        }
    }
}
