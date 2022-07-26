using System;
namespace ChatServer.Dtos.Users
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
