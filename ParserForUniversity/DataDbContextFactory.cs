using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ParserForUniversity
{
    public class DataDbContextFactory : IDesignTimeDbContextFactory<DataDbContext>
    {
        public DataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=avito-parser-db;Username=avito-parser;Password=avito-parser");

            return new DataDbContext(optionsBuilder.Options);
        }
    }
}