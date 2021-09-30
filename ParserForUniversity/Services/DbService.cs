using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        
    }
}