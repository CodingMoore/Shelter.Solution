using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shelter.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Shelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CatsController : ControllerBase
  {
    private ShelterContext _db;
    public CatsController(ShelterContext db)
    {
      _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cat>> Get(int catId, string catName, int catAge, string catBreed, string catSex)
    {
      var query = _db.Cats.AsQueryable();

      if (catId != 0)
      {
        query = query.Where(entry => entry.CatId == catId);
      }

      if (catName != null)
      {
        query = query.Where(entry => entry.CatName == catName);
      }

      if (catAge != 0)
      {
        query = query.Where(entry => entry.CatAge == catAge);
      }

      if (catBreed != null)
      {
        query = query.Where(entry => entry.CatBreed == catBreed);
      }

      if (catSex != null)
      {
        query = query.Where(entry => entry.CatSex == catSex);
      }

      return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Cat> Get(int id)
    {
      return _db.Cats.FirstOrDefault(entry => entry.CatId == id);
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
      cat.CatId = id;
      _db.Entry(cat).State = EntityState.Modified;
      _db.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var catToDelete = _db.Cats.FirstOrDefault(entry => entry.CatId == id);
      _db.Cats.Remove(catToDelete);
      _db.SaveChanges();
    }

  }
}