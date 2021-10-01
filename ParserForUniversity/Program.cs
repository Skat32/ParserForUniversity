using System;
using System.Threading.Tasks;
using Models.Entities;
using ParserForUniversity.Services;

namespace ParserForUniversity
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            // await ParseApi(args);
            await ParseHtml(args);
        }

        private static async Task ParseHtml(string[] args)
        {
             var parser = new ParserService(TypeParser.Html, args);
            
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("start parse RealEstateAbroad");
            //
            // if (!await parser.ParseAndSaveAsync(
            //     @"https://www.avito.ru/rossiya/nedvizhimost_za_rubezhom/sdam-ASgBAgICAUSaA_AQ?cd=1&f=ASgBAgICAkSaA_AQqgn8YA",
            //     TypeAdvertisement.RealEstateAbroad))
            //     return;
            //
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("end parse RealEstateAbroad");
            Console.WriteLine("start parse HousesOrCottages");

            if (!await parser.ParseAndSaveAsync(
                @"https://www.avito.ru/rossiya/doma_dachi_kottedzhi/sdam/posutochno-ASgBAgICAkSUA9IQoAjKVQ?cd=1",
                TypeAdvertisement.HousesOrCottages))
                return;
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("end parse HousesOrCottages");
            // Console.WriteLine("start parse Room");
            //
            // if (!await parser.ParseAndSaveAsync(
            //     @"https://www.avito.ru/rossiya/komnaty/sdam/posutochno/-ASgBAgICAkSQA74QqAn4YA?cd=1",
            //     TypeAdvertisement.Room))
            //     return;
            //
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("end parse Room");
            // Console.WriteLine("start parse Flat");
            //
            // if (!await parser.ParseAndSaveAsync(
            //     @"https://www.avito.ru/rossiya/kvartiry/sdam/posutochno/-ASgBAgICAkSSA8gQ8AeSUg",
            //     TypeAdvertisement.Flat))
            //     return;
            //
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("end parse Flat");
            // Console.WriteLine("start parse UserLink");
            //
            // await parser.ParseUserLinkAsync();
            //
            // Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("end parse UserLink");
            //
            // Console.ReadKey();Console.ReadKey();Console.ReadKey();Console.ReadKey();Console.ReadKey();
        }

        private static async Task ParseApi(string[] args)
        {
            var parser = new ParserService(TypeParser.Api, args);

            await parser.ParseAndSaveAsync(string.Empty, TypeAdvertisement.Flat);
            await parser.ParseAndSaveAsync(string.Empty, TypeAdvertisement.Room);
            await parser.ParseAndSaveAsync(string.Empty, TypeAdvertisement.HousesOrCottages);
        }
    }
}