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
    public ActionResult<IEnumerable<Dog>> Get(int id, string name, int age, string breed, string sex)
    {
      var query = _db.Dogs.AsQueryable();

      if (id != 0)
      {
        query = query.Where(entry => entry.Id == id);
      }

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (age != 0)
      {
        query = query.Where(entry => entry.Age == age);
      }

      if (breed != null)
      {
        query = query.Where(entry => entry.Breed == breed);
      }

      if (sex != null)
      {
        query = query.Where(entry => entry.Sex == sex);
      }

      return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Dog> Get(int id)
    {
      return _db.Dogs.FirstOrDefault(entry => entry.Id == id);
    }

    [HttpPost]
    public void Post([FromBody] Dog dog)
    {
      _db.Dogs.Add(dog);
      _db.SaveChanges();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Dog dog)
    {
      dog.Id = id;
      _db.Entry(dog).State = EntityState.Modified;
      _db.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var dogToDelete = _db.Dogs.FirstOrDefault(entry => entry.Id == id);
      _db.Dogs.Remove(dogToDelete);
      _db.SaveChanges();
    }

  }
}