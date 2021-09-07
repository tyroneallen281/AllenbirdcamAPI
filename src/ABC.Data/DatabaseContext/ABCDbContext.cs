using ABC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.Data.DatabaseContext
{
    public class ABCDbContext : DbContext
    {
        public ABCDbContext(DbContextOptions<ABCDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Vote> Votes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Sighting> Sightings { get; set; }
     
    }
}