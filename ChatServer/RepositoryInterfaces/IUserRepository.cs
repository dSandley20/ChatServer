using System;
using System.Collections.Generic;
using ChatServer.Dtos.User;
namespace ChatServer.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public void UpdateUser(UpdateUserDto userDetails, int userId);
        public void DeleteUser(Int32 id);
        public ClientUserDto GetUser(Int32 id);
        public List<ClientUserDto> GetUsers(int serverId);
        public void UpdateLastActive(int userId);
    }
}
