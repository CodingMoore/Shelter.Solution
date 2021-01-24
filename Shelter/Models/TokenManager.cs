using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shelter
{
  public class TokenManager
  {
    private static string Secret = Guid.NewGuid().ToString();
    public static string GenerateToken(string userName)
    {
      byte[] key = Convert.FromBase64String(Secret);
      SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
      SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims:new[] { new Claim(type: ClaimTypes.Name, value: userName)}),
        Expires = DateTime.UtcNow.AddMinutes(30),
        SigningCredentials = new SigningCredentials(securityKey, 
          algorith: SecurityAlgorithms.HmacSha256Signature)
      };
      JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
      JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
      return handler.WriteToken(token);
    }
  }
}