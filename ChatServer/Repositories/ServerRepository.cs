using System;
using System.Collections.Generic;
using ChatServer.Dtos.Server;
using ChatServer.Exceptions;
using ChatServer.RepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ChatServer.Repositories
{
    public class ServerRepository : IServerRepository
    {
        public readonly IConfiguration configuration;

        public NpgsqlConnection connection;

        public ServerRepository(IConfiguration _config)
        {
            configuration = _config;
            connection = new NpgsqlConnection(configuration["DB:connection"]);
        }

        public void CreateServer(CreateServerDto server)
        {
            try
            {
                var sql = @"INSERT INTO server (name) VALUES (@name)";
                connection.Execute(sql, new { name = server.name });
            }
            catch (Exception)
            {
                throw new ServerCreationError("Could not create the a new server");
            }
        }

        public void DeleteServer(int serverId)
        {
            var existingServer =
                connection
                    .QueryFirstOrDefault("SELECT * FROM server WHERE id = @id",
                    new { id = serverId });
            if (existingServer != null)
            {
                var sql = @"DELETE FROM server WHERE id = @id";
                connection.Execute(sql, new { id = serverId });
            }
            throw new ServerNotFound("Server does not exist");
        }

        public List<ClientServerDto> ListServer(int userId)
        {
            var sql =
                @"SELECT * FROM server s LEFT JOIN user_server us ON s.id = us.server WHERE us.user = @userId ";
            return connection
                .Query<ClientServerDto>(sql, new { userId = userId })
                .AsList();
        }

        public ClientServerDto JoinServer(JoinServerDto data, int userId)
        {
            var serverCheckSQL =
                @"SELECT * FROM server WHERE invite_code = @InviteCode";
            var existingServer =
                connection
                    .QueryFirstOrDefault<ClientServerDto>(serverCheckSQL,
                    new { InviteCode = data.invite_code });
            if (existingServer == null)
            {
                throw new ServerNotFound("You have not joined this server");
            }
            var addServerSQL =
                @"INSERT INTO user_server (user, server) VALUES (@User, @Server)";
            connection
                .Execute(addServerSQL,
                new { User = userId, Server = existingServer.Id });
            ConnectToServer(existingServer.Id, userId);
            return GetServer(existingServer.Id);
        }

        public ClientServerDto GetServer(int serverId)
        {
            try
            {
                var sql = @"SELECT * FROM server s WHERE s.id = @serverId ";
                return connection
                    .QueryFirstOrDefault(sql, new { serverId = serverId });
            }
            catch (Exception)
            {
                throw new ServerNotFound("No Server Found");  
            }
        }

        public void ConnectToServer(int serverId, int userId)
        {
            var checkUserServerSQL =
                @"SELECT * FROM user_server WHERE user = @User AND server = @Server";
            var existingUserServer =
                connection
                    .QueryFirstOrDefault<UserServerDto>(checkUserServerSQL,
                    new { User = userId, Server = serverId });
            if (existingUserServer == null)
            {
                throw new ServerNotFound("User can not connect to this server");
            }
            var updateUserServerLocation =
                @"INSERT INTO user_location ( user, server) VALUES ( @User, @Server), 
                ON CONFLICT (user) DO UPDATE 
                SET server = @Server";
            connection
                .Execute(updateUserServerLocation,
                new { User = userId, Server = serverId });
        }
    }
}
