using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ChatServer.Dtos.Users;
using ChatServer.Utilities;
using ChatServer.RepositoryInterfaces;

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
            this.configuration = _config;
            this.repository = _repo;
        }
    }
}
