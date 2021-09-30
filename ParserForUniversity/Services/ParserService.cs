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
        
        public async Task ParseAndSaveAsync(string url, TypeAdvertisement advertisement)
        {
            do
            {
                var result = await _parser.ParseAsync(url);

                await _dbService.SaveAdvertisementsAsync(new ApiModel(advertisement, result, url));
            } while (!string.IsNullOrEmpty(_parser.GetNexPageAsync().GetAwaiter().GetResult()));
        }

        public async Task ParseUserLinkAsync()
        {
            var advertisements = await _dbService.GetAdvertisementsUrlsAsync();
            
            
        }
    }
}