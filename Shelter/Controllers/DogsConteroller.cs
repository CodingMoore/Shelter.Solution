using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shelter.Models;
using Microsoft.EntityFrameworkCore;

namespace Shelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DogsController : ControllerBase
  {
    private ShelterContext _db;
    public DogsController(ShelterContext db)
    {
      _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Dog>> Get(int dogId, string dogName, int dogAge, string dogBreed, string dogSex)
    {
      var query = _db.Dogs.AsQueryable();

      if (dogId != 0)
      {
        query = query.Where(entry => entry.DogId == dogId);
      }

      if (dogName != null)
      {
        query = query.Where(entry => entry.DogName == dogName);
      }

      if (dogAge != 0)
      {
        query = query.Where(entry => entry.DogAge == dogAge);
      }

      if (dogBreed != null)
      {
        query = query.Where(entry => entry.DogBreed == dogBreed);
      }

      if (dogSex != null)
      {
        query = query.Where(entry => entry.DogSex == dogSex);
      }

      return query.ToList();
    }

  }
}