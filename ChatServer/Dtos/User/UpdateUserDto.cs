using System;
namespace ChatServer.Dtos.Users
{
    public class UpdateUserDto
    {
        public Int32 Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
