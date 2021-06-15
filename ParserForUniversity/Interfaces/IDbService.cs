using System.Threading.Tasks;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Interfaces
{
    public interface IDbService
    {
        Task SaveCommentsAsync(PostResponse postResponse);
    }
}