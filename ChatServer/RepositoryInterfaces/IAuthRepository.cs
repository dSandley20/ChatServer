using System;
using ChatServer.Dtos.Users;
namespace ChatServer.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        public string CreateUser(CreateUserDto userDetails);
        public void ForgotPassword(string email);
        public string ResetPassword(string pin, string password);
        public ClientUserDto Authenticate(UserLoginDto userDetails);
    }
}
