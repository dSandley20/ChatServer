using ChatServer.Dtos.User;

namespace ChatServer.Dtos.Auth
{
    public class AuthDto
    {
        public string token { get; set; }

        public ClientUserDto user { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserDto
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
