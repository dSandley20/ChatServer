using System;
using System.Collections.Generic;
using ChatServer.Dtos.Users;
namespace ChatServer.RepositoryInterfaces
{
    public interface IUserRepository
    {
        
        public void UpdateUser(UpdateUserDto userDetails);
        public void DeleteUser(Int32 id);
        public ClientUserDto GetUser(Int32 id);
        public List<ClientUserDto> GetUsers();
    }
}
