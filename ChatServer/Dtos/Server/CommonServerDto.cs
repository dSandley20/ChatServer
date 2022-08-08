using ChatServer.Dtos.User;

namespace ChatServer.Dtos.Server
{
    public class ClientServerDto
    {
        public int Id;

        public string Name;

        public ClientUserDto[] User;
    }

    public class CreateServerDto
    {
        public string name { get; set; }
    }

    public class JoinServerDto
    {
        public string invite_code { get; set; }
    }

    public class UserServerDto
    {
        public int Id { get; set; }

        public int User { get; set; }

        public int Server { get; set; }
    }
}
