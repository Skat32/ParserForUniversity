using ParserForUniversity.Models;

namespace ParserForUniversity.Interfaces
{
    public interface IParser
    {
        ParsedAdvertisement[] Parse(string urlToPost);

        string GetNexPage(int index);

        string ParseUserLink(string advertisement);
    }
}