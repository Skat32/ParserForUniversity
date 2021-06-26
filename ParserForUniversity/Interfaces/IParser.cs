using System.Collections.Generic;
using System.Threading.Tasks;
using ParserForUniversity.Models;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Interfaces
{
    public interface IParser
    {
        Task<IEnumerable<ParsedComments>> ParseAsync(string urlToPost);
    }
}