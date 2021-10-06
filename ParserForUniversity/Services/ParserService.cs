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

        private readonly TypeParser _typeParser;
        
        public ParserService(TypeParser parser, string[] args)
        {
            _dbService = Factory.GetDbService(args);
            _parser = Factory.GetParser(parser);
            _typeParser = parser;
        }
        
        public Task<bool> ParseAndSaveAsync(string url, TypeAdvertisement advertisement) =>
            _typeParser switch
            {
                TypeParser.Html => ParseHtml(url, advertisement),
                TypeParser.Api => ParseApi(advertisement),
                _ => throw new ArgumentOutOfRangeException()
            };

        private static string GetUrl(TypeAdvertisement advertisement, int page) =>
            advertisement switch
            {
                TypeAdvertisement.Flat =>
                    $"https://www.avito.ru/js/v2/map/items?categoryId=24&locationId=621540&correctorMode=0&page={page}&map=e30%3D&params%5B201%5D=1060&params%5B504%5D=5257&verticalCategoryId=1&rootCategoryId=4&viewPort%5Bwidth%5D=1291&viewPort%5Bheight%5D=691&limit=50",
                TypeAdvertisement.Room => $"https://www.avito.ru/js/v2/map/items?categoryId=23&locationId=621540&correctorMode=1&page={page}&map=e30%3D&params%5B200%5D=1055&params%5B596%5D=6204&verticalCategoryId=1&rootCategoryId=4&viewPort%5Bwidth%5D=699&viewPort%5Bheight%5D=660&limit=50",
                TypeAdvertisement.HousesOrCottages => $"https://www.avito.ru/js/v2/map/items?categoryId=25&locationId=621540&correctorMode=1&page={page}&map=e30%3D&params%5B202%5D=1065&params%5B528%5D=5477&verticalCategoryId=1&rootCategoryId=4&viewPort%5Bwidth%5D=699&viewPort%5Bheight%5D=660&limit=50",
                TypeAdvertisement.RealEstateAbroad => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(advertisement), advertisement, null)
            };
        
        private async Task<bool> ParseApi(TypeAdvertisement advertisement)
        {
            var i = 1;
            try
            {
                for (i = 1; i <= 100; i++)
                {
                    var url = GetUrl(advertisement, i);
                    var items = _parser.Parse(url);
                
                    await _dbService.SaveAdvertisementsAsync(new ApiModel(advertisement, items, url));
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(i);
                return false;
            }
            
        }
        
        private async Task<bool> ParseHtml(string url, TypeAdvertisement advertisement)
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
                try
                {
                    var result = _parser.ParseUserLink(advertisement);
                    
                    await _dbService.SaveUserLinkAsync(advertisement, result);
                }
                catch (Exception e)
                {
                    try
                    {
                        var result = _parser.ParseUserLink(advertisement);

                        await _dbService.SaveUserLinkAsync(advertisement, result);
                    }
                    catch 
                    {
                        continue;
                    }
                }

            }
        }
    }
}