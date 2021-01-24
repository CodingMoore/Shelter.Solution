using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;  //This doesn't exist yet
using WebApi.Entities;  //This doesn't exist yet

namespace WebApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;  //IUserService does not exist yet.
    public UsersController(IUserService userService)  //IUserService does not exist yet.
    {
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]User userParam) //User does not exist yet.
    {
      var user = _userService.Authenticate(userParam.Username, userParam.Password);

      if (user == null)
      {
        return BadRequest(new { message = "Username or password is incorrect"});
      }
      return Ok(user);
    }

  }
}

