using ChatServer.Dtos.User;
using ChatServer.Dtos.Auth;
namespace ChatServer.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        public AuthDto CreateUser(CreateUserDto userDetails);
        public void ForgotPassword(string email);
        public string ResetPassword(string pin, string password);
        public ClientUserDto Authenticate(UserLoginDto userDetails);
    }
}
