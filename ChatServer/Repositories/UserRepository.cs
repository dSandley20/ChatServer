using System;
using System.Collections.Generic;
using ChatServer.Dtos.Users;
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
            var sql = @"DELETE FROM users WHERE id = @id";
            connection.Execute(sql);
        }

        public ClientUserDto GetUser(int id)
        {
            return connection.QueryFirstOrDefault<ClientUserDto>("SELECT username, email FROM users WHERE id = @id");
        }

        public List<ClientUserDto> GetUsers()
        {
           return (List<ClientUserDto>)connection.Query<ClientUserDto>("SELECT username, email FROM users");
        }

        public void UpdateUser(UpdateUserDto userDetails)
        {
            //TODO generate new password using Bcrypt
            var sql = @"UPDATE users SET username = @userDetails.Username, email = @userDetails.Email WHERE id = @userDetails.Id";
            connection.Execute(sql);
        }

      
    }
}
