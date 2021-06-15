using System.Threading.Tasks;

namespace ParserForUniversity.Interfaces
{
    public interface IParserService
    {
        Task ParseAndSaveAsync(string url);
    }
}