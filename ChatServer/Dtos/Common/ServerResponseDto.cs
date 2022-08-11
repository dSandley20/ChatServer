using System;

namespace ChatServer.Dtos.Common
{
    public class ServerResponseDto
    {
        public string message { get; set; }

        public object data { get; set; }
    }

    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
