using System;
namespace ChatServer.Exceptions
{
    [Serializable]
    public class UserNotFound : Exception
    {
        public UserNotFound(string message) : base(message) { }
    }

    [Serializable]
    public class UserUpdateError : Exception
    {
        public UserUpdateError(string message): base(message) { }
    }

    [Serializable]
    public class UserCreateError : Exception
    {
        public UserCreateError(string message): base(message) { }
    }
}

