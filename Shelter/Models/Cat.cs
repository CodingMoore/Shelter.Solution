using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class Cat
  {
    public int Id { get; set; }
    [Required]
    [StringLength (150)]
    public string Name { get; set; }
    [Required]
    [StringLength (150)]
    public string Breed { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    [StringLength (50)]
    public string Sex { get; set; }

  }
}