using System.Threading.Tasks;
using Models.Entities;

namespace ParserForUniversity.Interfaces
{
    public interface IParserService
    {
        Task<bool> ParseAndSaveAsync(string url, TypeAdvertisement advertisement);
    }
}