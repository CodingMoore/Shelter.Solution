1. Create a UsersController.cs file (ProjectName.Solution/ProjectName/Controllers/UsersController.cs) and add the following code...

```
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

    [AllowAnonymous]  // This overrides the [Authorize] tag for this route only.
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
```

2. Create an Entity directory and User.cs file (ProjectName.Solution/ProjectName/Entities/User.cs) and add the following code...

note: FirstName and LastName are probably optional and I have commented them out for this exercise.

```
namespace WebApi.Entities
{
  public class User
  {
    public int Id { get; set; }
    // public string FirstName { get; set; } //Optional?
    // public string LastName { get; set; } //Optional?
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } //Optional if you want to have user rolls?
    public string Token { get; set; }
  }
}
```

3. Create a Helpers directory and an AppSettings.cs file (ProjectName.Solution/ProjectName/Helpers/AppSettings.cs) and add the following code...

```
namespace WebApi.Helpers
{
  public class AppSettings
  {
    public string Secret { get; set; }
  }
}
```

4. Create a Services directory and a UserServices.cs file (ProjectName.Solution/ProjectName/Services/UserServices.cs) and add the following code...

```

```
