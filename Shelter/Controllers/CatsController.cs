using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shelter.Models;
using Microsoft.EntityFrameworkCore;

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

  }
}