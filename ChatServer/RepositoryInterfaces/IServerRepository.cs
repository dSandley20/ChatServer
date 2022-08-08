using System.Collections.Generic;
using ChatServer.Dtos.Server;
namespace ChatServer.RepositoryInterfaces
{
    public interface IServerRepository
    {
        public void CreateServer(CreateServerDto server);
        public List<ClientServerDto> ListServer(int userId);
        public void DeleteServer(int serverId);
        public ClientServerDto JoinServer(JoinServerDto data, int userId);
        public ClientServerDto GetServer(int serverId);
        public void ConnectToServer(int serverId, int userId);

    }
}
