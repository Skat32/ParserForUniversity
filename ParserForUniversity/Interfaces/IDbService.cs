using System.Threading.Tasks;
using ParserForUniversity.Models;

namespace ParserForUniversity.Interfaces
{
    public interface IDbService
    {
        Task SaveCommentsAsync(ParsedComments[] comments, string postUrl);
    }
}