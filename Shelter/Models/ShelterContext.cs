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
                    new Cat { Id = 1, Name = "Alphred", Age = 6, Breed = "American Shorthair", Sex = "Female"},
                    new Cat { Id = 2, Name = "Unfried", Age = 7, Breed = "Bombay", Sex = "Male"},
                    new Cat { Id = 3, Name = "Hans", Age = 8, Breed = "Cyprus", Sex = "Female"},
                    new Cat { Id = 4, Name = "Gilbert", Age = 9, Breed = "Devon Rex", Sex = "Male"},
                    new Cat { Id = 5, Name = "Heathrow", Age = 10, Breed = "Egyptian Mau", Sex = "Female"}
                );
                builder.Entity<Dog>()
                .HasData(
                    new Dog { Id = 1, Name = "Higgins", Age = 6, Breed = "Akita", Sex = "Male"},
                    new Dog { Id = 2, Name = "Basil", Age = 7, Breed = "Belgian Shepherd", Sex = "Female"},
                    new Dog { Id = 3, Name = "Oliver", Age = 8, Breed = "Chilean Terrier", Sex = "Male"},
                    new Dog { Id = 4, Name = "Hammond", Age = 9, Breed = "Dingo", Sex = "Female"},
                    new Dog { Id = 5, Name = "Sigfried", Age = 10, Breed = "Estonian Hound", Sex = "Male"}
                );
        }


    }
}
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.Entity<Dog>()
        //         .HasData(
        //             new Dog { DogId = 1, DogName = "Higgins", DogAge = 6, DogBreed = "Akita", DogSex = "Male"},
        //             new Dog { DogId = 2, DogName = "Basil", DogAge = 7, DogBreed = "Belgian Shepherd", DogSex = "Female"},
        //             new Dog { DogId = 3, DogName = "Oliver", DogAge = 8, DogBreed = "Chilean Terrier", DogSex = "Male"},
        //             new Dog { DogId = 4, DogName = "Hammond", DogAge = 9, DogBreed = "Dingo", DogSex = "Female"},
        //             new Dog { DogId = 5, DogName = "Sigfried", DogAge = 10, DogBreed = "Estonian Hound", DogSex = "Male"}

        //         );
        // }