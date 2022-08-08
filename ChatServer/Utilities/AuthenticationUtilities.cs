using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ChatServer.Dtos.Auth;
using ChatServer.Dtos.User;
using ChatServer.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChatServer.Utilities
{
    public class AuthenticationUtilities
    {
        //This is purely for testing purposes using mock data
        public static ClientUserDto Authenticate(UserLoginDto user)
        {
            //TODO query the database instead of using mocks
            var existingUser =
                UserMocks
                    .users
                    .FirstOrDefault(u =>
                        u.Username == user.Username &&
                        u.Password == user.Password);
            if (existingUser != null)
            {
                return new ClientUserDto()
                {
                    Email = existingUser.Email,
                    Username = existingUser.Username
                };
            }
            return null;
        }

        public static string
        GenerateToken(ClientUserDto user, IConfiguration configuration)
        {
            if (user == null)
            {
                return null;
            }

            var securityKey =
                new SymmetricSecurityKey(Encoding
                        .UTF8
                        .GetBytes(configuration["Jwt:Key"]));
            var credentials =
                new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256);

            var claims =
                new Claim[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            var token =
                new JwtSecurityToken(configuration["Jwt:Issues"],
                    configuration["Jwt:Audiance"],
                    claims,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
