using System;
using System.Collections.Generic;
using ChatServer.Dtos.User;
using ChatServer.Exceptions;
using ChatServer.RepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ChatServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly IConfiguration configuration;

        public NpgsqlConnection connection;

        public UserRepository(IConfiguration _config)
        {
            configuration = _config;
            connection = new NpgsqlConnection(configuration["DB:connection"]);
        }

        public void DeleteUser(int id)
        {
            try
            {
                var existingUser =
                    connection
                        .QueryFirstOrDefault
                        <ClientUserDto>("SELECT * FROM users FROM id = @Id",
                        new { Id = id });
                var sql = @"DELETE FROM users WHERE id = @id";
                connection.Execute (sql);
            }
            catch (Exception)
            {
                throw new UserNotFound("No User to Delete");
            }
        }

        public ClientUserDto GetUser(int id)
        {
            try
            {
                return connection
                    .QueryFirstOrDefault
                    <ClientUserDto
                    >("SELECT username, email FROM users WHERE id = @id");
            }
            catch (Exception)
            {
                throw new UserNotFound("No User Found");
            }
        }

        public List<ClientUserDto> GetUsers(int serverId)
        {
            try
            {
                return (List<ClientUserDto>)
                connection
                    .Query
                    <ClientUserDto
                    >("SELECT username, email FROM users JOIN user_server users.id = user_server.user WHERE user_server.server = @Server",
                    new { Server = serverId });
            }
            catch (Exception)
            {
                throw new UserNotFound("No users in the server");
            }
        }

        public void UpdateUser(UpdateUserDto userDetails, int userId)
        {
            var sql =
                @"UPDATE users SET username = @Username, email = @Email WHERE id = @Id";
            connection
                .Execute(sql,
                new {
                    Username = userDetails.Username,
                    Email = userDetails.Email,
                    Id = userId
                });
        }

        public void UpdateLastActive(int userId)
        {
            var sql = @"UPDATE users SET last_active = @time";
            connection.Execute(sql, new { time = DateTime.UtcNow });
        }
    }
}
