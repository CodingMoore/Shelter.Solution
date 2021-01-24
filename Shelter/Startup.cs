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