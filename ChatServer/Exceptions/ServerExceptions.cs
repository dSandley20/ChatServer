using System;
namespace ChatServer.Exceptions
{
    [Serializable]
    public class ServerNotFound : Exception
    {
        public ServerNotFound(string message) : base(message){}
    }

    [Serializable]
    public class ServerCreationError : Exception
    {
        public ServerCreationError(string message) : base(message) { }
    }

}
