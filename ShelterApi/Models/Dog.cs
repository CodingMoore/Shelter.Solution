using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class Dog
  {
    public int DogId { get; set; }
    [Required]
    [StringLength (150)]
    public string DogName { get; set; }
    [Required]
    [StringLength (150)]
    public string DogBreed { get; set; }
    [Required]
    public int DogAge { get; set; }
    [Required]
    [StringLength (50)]
    public string DogSex { get; set; }

  }
}