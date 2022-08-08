using System;
using ChatServer.Enums;

namespace ChatServer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set;}
        public string Password { get; set; }
        public UserStatus Status {get; set;}
        public DateTime LastActive {get; set;}
    }
}
