using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Models;

namespace ParserForUniversity.Services
{
    public class DbService : IDbService
    {
        private readonly DataDbContext _context;
        
        public DbService(string[] args)
        {
            _context = new DataDbContextFactory().CreateDbContext(args);;
        }

        public async Task SaveAdvertisementsAsync(ApiModel data)
        {
            await _context.Advertisements
                .AddRangeAsync(
                    data.ParsedAdvertisements
                        .GroupBy(x => x.Url).Select(x => x.First()).ToList()
                        .Where(advertisement => _context.Advertisements.All(x => x.Url != advertisement.Url))
                        .Select(x => new Advertisement(x.Url, x.UrlToUser, data.TypeAdvertisement, data.UrlByParsed)
               ));

            await _context.SaveChangesAsync();
        }

        public async Task<string[]> GetAdvertisementsUrlsAsync()
        {
            return await _context.Advertisements.Where(x => x.UrlToUser == null).Select(x => x.Url).ToArrayAsync();
        }
    }
}