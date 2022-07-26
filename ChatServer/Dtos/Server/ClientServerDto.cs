using System;
using ChatServer.Dtos.Users;
namespace ChatServer.Dtos.Server
{
    public class ClientServerDto
    {
        public int Id;
        public string Name;
        public ClientUserDto[] User;
    }
}
