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
                    new Cat { CatId = 1, CatName = Alphred, CatAge = 6, CatBreed = American Shorthair, CatSex = Female},
                    new Cat { CatId = 2, CatName = Unfried, CatAge = 7, CatBreed = Bombay, CatSex = Male},
                    new Cat { CatId = 3, CatName = Hans, CatAge = 8, CatBreed = Cyprus, CatSex = Female},
                    new Cat { CatId = 4, CatName = Gilbert, CatAge = 9, CatBreed = Devon Rex, CatSex = Male},
                    new Cat { CatId = 5, CatName = Heathrow, CatAge = 10, CatBreed = Egyptian Mau, CatSex = Female}
                );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Dog>()
                .HasData(
                    new Dog { DogId = 1, DogName = Higgins, DogAge = 6, DogBreed = Akita, DogSex = Male},
                    new Dog { DogId = 2, DogName = Basil, DogAge = 7, DogBreed = Belgian Shepherd, DogSex = Female},
                    new Dog { DogId = 3, DogName = Oliver, DogAge = 8, DogBreed = Chilean Terrier, DogSex = Male},
                    new Dog { DogId = 4, DogName = Hammond, DogAge = 9, DogBreed = Dingo, DogSex = Female},
                    new Dog { DogId = 5, DogName = Sigfried, DogAge = 10, DogBreed = Estonian Hound, DogSex = Male}

                );
        }

    }
}