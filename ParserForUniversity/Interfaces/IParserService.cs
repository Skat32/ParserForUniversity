using System.Threading.Tasks;
using Models.Entities;

namespace ParserForUniversity.Interfaces
{
    public interface IParserService
    {
        Task ParseAndSaveAsync(string url, TypeAdvertisement advertisement);
    }
}