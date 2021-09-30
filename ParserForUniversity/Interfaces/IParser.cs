using System.Threading.Tasks;
using ParserForUniversity.Models;

namespace ParserForUniversity.Interfaces
{
    public interface IParser
    {
        Task<ParsedAdvertisement[]> ParseAsync(string urlToPost);

        Task<string> GetNexPageAsync();
    }
}