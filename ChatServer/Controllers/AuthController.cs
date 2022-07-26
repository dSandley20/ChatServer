using System;
using ChatServer.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ChatServer.Dtos.Users;
using ChatServer.Utilities;
using Microsoft.AspNetCore.Authorization;

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
            if(existingUser != null)
            {
                return  Ok(AuthenticationUtilities.GenerateToken(existingUser, configuration));
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateAccount(CreateUserDto userDetails)
        {
            var token = repository.CreateUser(userDetails);
            if(token != null)
            {
                return Ok(token);
            }
            return StatusCode(500, "Could not create a new user");
        }
    }
}
