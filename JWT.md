This Guide is assuming that you have already built out an API (probably using the "dotnet new webapi --framework netcoreapp2.2" command).  To add JSON Web Token Support, follow the steps below.  

Please note that in this example, the Uername and Password are not being stored in a secure Hash in a database, but written in plain text in the "UserServices.cs" file.  __This is probably a terrible apractice!__

Also note that some of these files are new, and all of the file code is written out.  Other files may only show modified code or show comments where new code is added.

Also also note, __"Shelter"__ is the name of the project, and thus the standard namespace for files.

Lastly, this was made following this guide: https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api#users-controller-cs


1. Create a UsersController.cs file (ProjectName.Solution/ProjectName/Controllers/UsersController.cs) and add the following code...

```
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shelter.Services;  //This doesn't exist yet
using Shelter.Entities;  //This doesn't exist yet

namespace Shelter.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api/[controller]")]
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

2. Create an Entities directory and User.cs file (ProjectName.Solution/ProjectName/Entities/User.cs) and add the following code...

note: FirstName and LastName are probably optional and I have commented them out for this exercise.

```
namespace Shelter.Entities
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
namespace Shelter.Helpers
{
  public class AppSettings
  {
    public string Secret { get; set; }
  }
}
```

4. Create a Services directory and a UserServices.cs file (ProjectName.Solution/ProjectName/Services/UserServices.cs) and add the following code...

```
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shelter.Entities;
using Shelter.Helpers;

namespace Shelter.Services
{
  public interface IUserService
  {
    User Authenticate(string username, string password);
    IEnumerable<User> GetAll();
  }

  public class UserService : IUserService
  {
    private List<User> _users = new List<User>
    {
    new User  { Id = 1, Username = "test", Password = "test" }
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    public User Authenticate(string username, string password)
    {
      var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

      if (user == null)
      {
        return null;
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        Expires = DateTime.UtcNow.AddHours(24),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      user.Password = null;

      return user;

    }

    public IEnumerable<User> GetAll()
    {
      return _users.Select(x => {
        x.Password = null;
        return x;
      });
    }

  }
}
```

5. __Add__ the following "AppSettings" code to the top of your appsettings.json file.  __You need to change the "Secret" String to something else.__

```
{
  "AppSettings": {
    "Secret": "THIS IS USED TO SIGN IN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING"
  },
 
  ... 

}
```

6. Add the following code to Startup.cs...

```
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer; //NEW JWT CODE!!!
using Microsoft.IdentityModel.Tokens; //NEW JWT CODE!!!
using System.Text; //NEW JWT CODE!!!
using Shelter.Helpers;  //NEW JWT CODE!!!
using Shelter.Services;  //NEW JWT CODE!!!

namespace Shelter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();  //Only if you are using Swagger
            services.AddCors(); // Only if you are using CORS
            services.AddDbContext<ShelterContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettingsSection = Configuration.GetSection("AppSettings"); // NEW CODE
            services.Configure<AppSettings>(appSettingsSection); // NEW CODE

            //JWT CODE BELOW/////////////////////////////////////////////
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>  // I don't really understand what these boolian settings are doing and thus I don't know if they should be changed.
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //NEW JWT CODE ABOVE////////////////////////////////////////////

            services.AddScoped<IUserService, UserService>(); // NEW CODE
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(); // Only if using Swagger

            app.UseSwaggerUI(c => // Only if using Swagger
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(x => x // Only if you are using CORS
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication(); //NEW JWT CODE!!(Before "app.UseMvc());
            app.UseMvc();

        }
        
    }
}
```

7. Add [Authorize]/[AllowAnonymous] tags to class controllers where you want to restrict or allow access to CRUD functionality.

8. Save and Run (Cross your fingers too)

9. Test your JWT by opening Postman and sending a GET request to an unrestricted API route to make sure things are sill connecting.

10. Next send a POST request and verify that you receive a "401 unauthorized" error.  This means your Authorization class/route tags are working.

11. In Postman, go to your Authorization URL.  In my case, it is  [http://localhost:5000/api/users/authenticate] since my UsersController.cs looks like this...

```
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shelter.Services; 
using Shelter.Entities; 

namespace Shelter.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api/[controller]")] //referencing this for our authentication request
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    public UsersController(IUserService userService)
    {
      _userService = userService;
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]User userParam) // referencing this for our authentication request
    {
      var user = _userService.Authenticate(userParam.Username, userParam.Password);

      if (user == null)
      {
        return BadRequest(new { message = "Username or password is incorrect"});
      }
      return Ok(user);
    }
    ...

```

Set request type to POST and put the following in the Body and send the request. The Username and Password are both set to "test" in this example, and can be changed in the UserServices.cs file.  However you SHOULD find a way to store these safely hashed in your database.  I dunno how to do that yet :(

```
    {
        "Username": "test",
        "Password": "test"
    }
```

You should receive a reply that contains your token, like.....

```
{
    "id": 1,
    "username": "test",
    "password": null,
    "role": null,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE2MTE1MTMwNDMsImV4cCI6MTYxMTU5OTQ0MywiaWF0IjoxNjExNTEzMDQzfQ.8xlwNGS_B1by6CxLebbpFtzZBRFU8aD7IC-T9_t_9Qk"
}
```

12. Click on "Authorization" in Postman (to the left of "Body") and use the dropdown menu to change the Type to "Bearer Token".
Copy the token from your authorization request (without quotes), into the Token input box.

13. Test to see if the token is working by submitting a POST, PUT, or Delete, request.  In theory, you should be able to do these CRUD actions now.  You can add or remove the JWT Token by switching between the "No Auth" and the "Bearer Token" Types in the Authorization dropdown menu.

14. Rejoice