using Microsoft.EntityFrameworkCore;
using ScrappyBot.Models;

namespace ScrappyBot.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<SpecificProduct> SpecificProduct { get; set; }

    }
}
