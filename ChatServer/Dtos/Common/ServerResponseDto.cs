using System;
namespace ChatServer.Dtos.Common
{
    public class ServerResponseDto
    {
        public string message { get; set; }
        public object data { get; set; }
    }
}
