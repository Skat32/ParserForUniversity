using ParserForUniversity.Services;

namespace ParserForUniversity
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new ParserService(TypeParser.Api, args);
            test.ParseAndSaveAsync(@"https://m.habr.com/ru/company/macloud/blog/562586/").GetAwaiter().GetResult();
        }
    }
}