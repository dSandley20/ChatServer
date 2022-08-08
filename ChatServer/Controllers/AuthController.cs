using ChatServer.Dtos.Auth;
using ChatServer.RepositoryInterfaces;
using ChatServer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly IAuthRepository repository;

        public AuthController(IConfiguration _config, IAuthRepository _repo)
        {
            this.configuration = _config;
            this.repository = _repo;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth(UserLoginDto userDetails)
        {
            var existingUser = repository.Authenticate(userDetails);
            if (existingUser != null)
            {
                var tokenString =
                    AuthenticationUtilities
                        .GenerateToken(existingUser, configuration);
                var token =
                    new AuthDto() { token = tokenString, user = existingUser };
                return Ok(token);
            }
            return StatusCode(404,
            new { message = "No user found with these credentials" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateAccount(CreateUserDto userDetails)
        {
            var token = repository.CreateUser(userDetails);
            if (token != null)
            {
                return Ok(token);
            }
            return StatusCode(500,
            new { message = "Could not create a new user" });
        }
    }
}
