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
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
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

5. Add the following "AppSettings" code to the top of your appsettings.json file.  __You need to change the "Secret" String to something else.__

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
using Microsoft.AspNetCore.Authentication.JwtBearer; //JWT CODE!!!
using Microsoft.IdentityModel.Tokens; //JWT CODE!!!
using System.Text; //JWT CODE!!!
using Shelter.Helpers;  //JWT CODE!!!
using Shelter.Services;  //JWT CODE!!!

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
            services.AddCors(); // Optional? maybe?
            services.AddDbContext<ShelterContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettingsSection = Configuration.GetSection("AppSettings"); // NEW CODE
            services.Configure<AppSettings>(appSettingsSection); // NEW CODE

            //JWT CODE BELOW/////////
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
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
            //JWT CODE ABOVE/////////

            services.AddScoped<IUserService, UserService>(); // NEW
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

            app.UseCors(x => x // Optional? maybe?
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication(); //JWT CODE!!(Before "app.UseMvc());
            app.UseMvc();

        }
        
    }
}
```

7. Add [Authorize]/[AllowAnonymous] tags to class controllers
