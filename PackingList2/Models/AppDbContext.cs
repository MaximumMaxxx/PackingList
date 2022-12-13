using Microsoft.EntityFrameworkCore;

namespace PackingList2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PackingList.db");
        }

        public DbSet<Item> Items { get; set; }
    }
}