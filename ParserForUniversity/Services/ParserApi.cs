using System;
using System.Collections.Generic;
using System.Linq;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserApi : IParser
    {
        private string url = "https://www.avito.ru/js/v2/map/items?categoryId=24&locationId=621540&correctorMode=0&page=100&map=e30%3D&params%5B201%5D=1060&params%5B504%5D=5257&verticalCategoryId=1&rootCategoryId=4&viewPort%5Bwidth%5D=1291&viewPort%5Bheight%5D=691&limit=50";

        private readonly HttpClientProxy _httpClient;
        
        public ParserApi()
        {
            _httpClient = new HttpClientProxy();
        }
        
        public ParsedAdvertisement[] Parse(string urlToPost)
        {
            var items = _httpClient.GetAsync<ApiAdvertisement>(urlToPost).GetAwaiter().GetResult();

            return items.Models.Select(x => new ParsedAdvertisement(x.Url, default)).ToArray();
        }

        public string GetNexPage(int index)
        {
            throw new NotImplementedException();
        }

        public string ParseUserLink(string advertisement)
        {
            throw new NotImplementedException();
        }
    }
}