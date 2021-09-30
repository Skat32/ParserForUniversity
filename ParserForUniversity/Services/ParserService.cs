using System;
using System.Threading.Tasks;
using Models.Entities;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class ParserService : IParserService
    {
        private readonly IDbService _dbService;
        private readonly IParser _parser;
        
        public ParserService(TypeParser parser, string[] args)
        {
            _dbService = Factory.GetDbService(args);
            _parser = Factory.GetParser(parser);
        }
        
        public async Task<bool> ParseAndSaveAsync(string url, TypeAdvertisement advertisement)
        {
            var i = 0;
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Start PARSE {url}");
                    var result = _parser.Parse(url);
                    Console.WriteLine($"End PARSE {url}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Start SAVE {url}");
                    await _dbService.SaveAdvertisementsAsync(new ApiModel(advertisement, result, url));
                    Console.WriteLine($"End SAVE {url}");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("START NEXT PAGE");
                    url = _parser.GetNexPage(i);
                    i = 1;
                    Console.WriteLine("END NEXT PAGE");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error by parse {url}");
                    Console.WriteLine($"Error: {e.Message}");
                    return false;
                }
         
            } while (!string.IsNullOrEmpty(url));

            return true;
        }

        public async Task ParseUserLinkAsync()
        {
            var advertisements = await _dbService.GetAdvertisementsUrlsAsync();

            foreach (var advertisement in advertisements)
            {
                var result = _parser.ParseUserLink(advertisement);

                await _dbService.SaveUserLinkAsync(advertisement, result);
            }
        }
    }
}