using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shelter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization; //JWT CODE!!!

namespace Shelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  // [Authorize]  //JWT CODE!!!
  public class CatsController : ControllerBase
  {
    private ShelterContext _db;
    public CatsController(ShelterContext db)
    {
      _db = db;
    }

    // [AllowAnonymous]  //JWT CODE!!!
    [HttpGet]
    public ActionResult<IEnumerable<Cat>> Get(int id, string name, int age, string breed, string sex)
    {
      var query = _db.Cats.AsQueryable();

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

    // [AllowAnonymous] //JWT CODE!!!
    [HttpGet("{id}")]
    public ActionResult<Cat> Get(int id)
    {
      return _db.Cats.FirstOrDefault(entry => entry.Id == id);
    }

    [HttpPost]
    public void Post([FromBody] Cat cat)
    {
      _db.Cats.Add(cat);
      _db.SaveChanges();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Cat cat)
    {
      cat.Id = id;
      _db.Entry(cat).State = EntityState.Modified;
      _db.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var catToDelete = _db.Cats.FirstOrDefault(entry => entry.Id == id);
      _db.Cats.Remove(catToDelete);
      _db.SaveChanges();
    }

  }
}