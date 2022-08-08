using System;
using System.Linq;
using System.Security.Claims;
using ChatServer.Dtos.User;
using ChatServer.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly IUserRepository repository;

        public UsersController(IConfiguration _config, IUserRepository _repo)
        {
            configuration = _config;
            repository = _repo;
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUser(UpdateUserDto userDetails)
        {
            var user = getUser();
            repository.UpdateUser(userDetails, user.Id);
            return Ok();
        }

        public ClientUserDto getUser()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaim = identity.Claims;
                return new ClientUserDto()
                {
                    Id =
                        Convert
                            .ToInt32(userClaim
                                .FirstOrDefault(o => o.Type == "Id")?
                                .Value),
                    Username =
                        userClaim
                            .FirstOrDefault(o =>
                                o.Type == ClaimTypes.NameIdentifier)?
                            .Value,
                    Email =
                        userClaim
                            .FirstOrDefault(o => o.Type == ClaimTypes.Email)?
                            .Value
                };
            }
            return null;
        }
    }
}
