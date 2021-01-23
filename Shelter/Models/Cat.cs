using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class Cat
  {
    public int CatId { get; set; }
    [Required]
    [StringLength (150)]
    public string CatName { get; set; }
    [Required]
    [StringLength (150)]
    public string CatBreed { get; set; }
    [Required]
    public int CatAge { get; set; }
    [Required]
    [StringLength (50)]
    public string CatSex { get; set; }

  }
}