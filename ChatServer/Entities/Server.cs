using System;
namespace ChatServer.Entities
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InviteOnly { get; set; }
        public User[] Users { get; set; }
    }
}
