using EnrichmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrichmentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }  
    }
}
