using System.Collections.Generic;
using System.Threading.Tasks;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Services
{
    public class ParserHtml : IParser
    {
        public Task<IEnumerable<ParsedComments>> ParseAsync(string urlToPost)
        {
           
            
            
            throw new System.NotImplementedException();
        }
    }
}