using Models.Entities;

namespace ParserForUniversity.Models
{
    public class ApiModel
    {
        public ApiModel(TypeAdvertisement typeAdvertisement, ParsedAdvertisement[] parsedAdvertisements, string urlByParsed)
        {
            TypeAdvertisement = typeAdvertisement;
            ParsedAdvertisements = parsedAdvertisements;
            UrlByParsed = urlByParsed;
        }

        public ParsedAdvertisement[] ParsedAdvertisements { get; private set; }

        public TypeAdvertisement TypeAdvertisement { get; private set; }
        
        /// <summary>
        /// Ссылка на которой спарсили данное объявление
        /// </summary>
        public string UrlByParsed { get; private set; }
    }

    public class ParsedAdvertisement
    {
        /// <summary>
        /// Ссылка на объявление
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public string? UrlToUser { get; private set; }

        public ParsedAdvertisement(string url, string? urlToUser)
        {
            Url = url;
            UrlToUser = urlToUser;
        }
    }
}