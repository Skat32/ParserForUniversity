using System.Threading.Tasks;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Interfaces
{
    public interface IParser
    {
        Task<PostResponse> ParseAsync(string urlToPost);
    }
}