using System;
using ChatServer.Dtos.Users;
using ChatServer.RepositoryInterfaces;
using ChatServer.Utilities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ChatServer.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        public readonly IConfiguration configuration;
        public NpgsqlConnection connection;

        public AuthRepository(IConfiguration _config) 
        {
            configuration = _config;
            connection = new NpgsqlConnection(configuration["DB:connection"]);
        }

        public ClientUserDto Authenticate(UserLoginDto userDetails)
        {
            var existingData = connection.QueryFirstOrDefault("SELECT id, password FROM users WHERE username = @Username", new { Username = userDetails.Username });
            if (BCryptUtilities.verifyPassword(userDetails.Password, existingData.password))
            {
                return connection.QueryFirstOrDefault<ClientUserDto>("SELECT Id , username, email FROM users WHERE id = @Id", new { Id = existingData.id });
            }
                return null;
        }

        public string CreateUser(CreateUserDto userDetails)
        {
            var sql = @"INSERT INTO users (username, email, password) VALUES (@Username, @Email, @Password)";
            connection.Execute(sql, new { Username = userDetails.Username, Email = userDetails.Email, Password = BCryptUtilities.hashPassword(userDetails.Password)});
            var loginInfo = new UserLoginDto() { Username = userDetails.Username, Password = userDetails.Password };
            return AuthenticationUtilities.GenerateToken(Authenticate(loginInfo), configuration);

        }

        public void ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(string pin, string password)
        {
            throw new NotImplementedException();
        }
    }
}
