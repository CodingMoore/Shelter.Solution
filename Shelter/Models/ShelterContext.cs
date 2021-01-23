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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cat>()
                .HasData(
                    new Cat { CatId = 1, CatName = , CatAge = , CatBreed = , CatSex = },
                    new Cat { CatId = 2, CatName = , CatAge = , CatBreed = , CatSex = },
                    new Cat { CatId = 3, CatName = , CatAge = , CatBreed = , CatSex = },
                    new Cat { CatId = 4, CatName = , CatAge = , CatBreed = , CatSex = },
                    new Cat { CatId = 5, CatName = , CatAge = , CatBreed = , CatSex = }
                );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Dog>()
                .HasData(
                    new Dog { DogId = 1, DogName = , DogAge = , DogBreed = , DogSex = },
                    new Dog { DogId = 2, DogName = , DogAge = , DogBreed = , DogSex = },
                    new Dog { DogId = 3, DogName = , DogAge = , DogBreed = , DogSex = },
                    new Dog { DogId = 4, DogName = , DogAge = , DogBreed = , DogSex = },
                    new Dog { DogId = 5, DogName = , DogAge = , CatBreed = , CatSex = }

                );
        }

    }
}