using System.Threading.Tasks;
using DataLayer;
using ParserForUniversity.Interfaces;
using ParserForUniversity.Responses;

namespace ParserForUniversity.Services
{
    public class DbService : IDbService
    {
        private readonly DataDbContext _context;
        
        public DbService(string connectionString)
        {
            _context = DataDbContext.CreateDbContext(connectionString);
        }
        
        public Task SaveCommentsAsync(PostResponse postResponse)
        {
            throw new System.NotImplementedException();
        }
    }
}