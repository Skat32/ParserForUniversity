using System;
using System.Threading.Tasks;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserApi : IParser
    {
        public Task<ParsedAdvertisement[]> ParseAsync(string urlToPost)
        {
            throw new NotImplementedException();
        }
    }
}