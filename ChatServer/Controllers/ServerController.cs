using System;
using System.Linq;
using System.Security.Claims;
using ChatServer.Dtos.Common;
using ChatServer.Dtos.Server;
using ChatServer.Dtos.User;
using ChatServer.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("/server")]
    public class ServerController : ControllerBase
    {
        public readonly IConfiguration configuration;

        public readonly IServerRepository repository;

        public ServerController(IConfiguration config, IServerRepository repo)
        {
            configuration = config;
            repository = repo;
        }

        //NOTE: only doing create and list for now
        [HttpGet]
        public ServerResponseDto GetServers()
        {
            var user = GetUser();
            return new ServerResponseDto()
            {
                message = "Servers Found",
                data = repository.ListServer(user.Id)
            };
        }

        [HttpPost("join")]
        public ClientServerDto JoinServer(JoinServerDto data)
        {
            var user = GetUser();

            return repository.JoinServer(data, user.Id);
        }

        [HttpPut("connect/{serverId}")]
        public ActionResult ConnectToServer(int serverId)
        {
            var user = GetUser();

            // TODO check to make sure that user has server in his list
            repository.ConnectToServer(serverId, user.Id);
            return Ok();
        }

        public ClientUserDto GetUser()
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
