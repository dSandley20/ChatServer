using System;
using ChatServer.Dtos.Auth;
using ChatServer.Dtos.User;
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
            var existingData =
                connection
                    .QueryFirstOrDefault("SELECT id, password FROM users WHERE username = @Username",
                    new { Username = userDetails.Username });
            if (existingData == null)
            {
                return null;
            }
            if (
                BCryptUtilities
                    .verifyPassword(userDetails.Password, existingData.password)
            )
            {
                var updateLastActive = @"UPDATE users SET last_active = @time";
                connection
                    .Execute(updateLastActive, new { time = DateTime.UtcNow });
                return connection
                    .QueryFirstOrDefault
                    <ClientUserDto
                    >("SELECT Id , username, email FROM users WHERE id = @Id",
                    new { Id = existingData.id });
            }
            return null;
        }

        public AuthDto CreateUser(CreateUserDto userDetails)
        {
            var sql =
                @"INSERT INTO users (username, email, password) VALUES (@Username, @Email, @Password)";
            connection
                .Execute(sql,
                new {
                    Username = userDetails.Username,
                    Email = userDetails.Email,
                    Password =
                        BCryptUtilities.hashPassword(userDetails.Password)
                });
            var loginInfo =
                new UserLoginDto()
                {
                    Username = userDetails.Username,
                    Password = userDetails.Password
                };
            var authenticatedUser = Authenticate(loginInfo);
            if (authenticatedUser == null)
            {
                return null;
            }
            var generatedToken =
                AuthenticationUtilities
                    .GenerateToken(authenticatedUser, configuration);
            return new AuthDto()
            { token = generatedToken, user = authenticatedUser };
        }

        public void ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(string pin, string password)
        {
            throw new NotImplementedException();
        }

        ClientUserDto IAuthRepository.Authenticate(UserLoginDto userDetails)
        {
            throw new NotImplementedException();
        }
    }
}
