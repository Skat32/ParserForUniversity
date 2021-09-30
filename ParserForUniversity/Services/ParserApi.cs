using System;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserApi : IParser
    {
        public ParsedAdvertisement[] Parse(string urlToPost)
        {
            throw new NotImplementedException();
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