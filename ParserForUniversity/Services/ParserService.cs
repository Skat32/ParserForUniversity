using System.Threading.Tasks;
using ParserForUniversity.Interfaces;

namespace ParserForUniversity.Services
{
    public class ParserService : IParserService
    {
        private readonly IDbService _dbService;
        private readonly IParser _parser;
        
        public ParserService(TypeParser parser)
        {
            _dbService = Factory.GetDbService("");
            _parser = Factory.GetParser(parser);
        }
        
        public async Task ParseAndSaveAsync(string url)
        {
            var result = await _parser.ParseAsync(url);
            await _dbService.SaveCommentsAsync(result);
        }
    }
}