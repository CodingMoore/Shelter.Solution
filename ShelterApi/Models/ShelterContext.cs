using Microsoft.EntityFrameworkCore;

namespace Shelter.Models
{
    public class ShelterContext : DbContext
    {
        public ShelterContext(DbContextOptions<ShelterContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Dog> Dogs { get; set; }
    }
}