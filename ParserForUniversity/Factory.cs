using System;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Services;

namespace ParserForUniversity
{
    public static class Factory
    {
        /// <summary>
        /// Получаем парсер выбранного типа
        /// </summary>
        public static IParser GetParser(TypeParser parser) => parser switch
        {
            TypeParser.Html => new ParserHtml(),
            TypeParser.Api => new ParserApi(),
            _ => throw new ArgumentOutOfRangeException(nameof(parser), parser, null)
        };

        public static IDbService GetDbService(string connectionString)
        {
            return new DbService(connectionString);
        }
    }
}