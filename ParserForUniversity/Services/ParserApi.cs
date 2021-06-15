using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Flurl.Http;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Services
{
    public class ParserApi : IParser
    {
        public async Task<PostResponse> ParseAsync(string urlToPost)
        {
            var postNumber = Regex.Match(urlToPost, @"\/([0-9]+)").Groups[1];
            var url = $"https://m.habr.com/kek/v2/articles/{postNumber}/comments/?fl=ru&hl=ru";

            var result = await url.GetJsonAsync<ApiModels>();
            
            throw new System.NotImplementedException();
        }
    }
}