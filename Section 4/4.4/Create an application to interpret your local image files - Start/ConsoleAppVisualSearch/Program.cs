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
            Console.WriteLine("Hello World");
        }
    }
}
