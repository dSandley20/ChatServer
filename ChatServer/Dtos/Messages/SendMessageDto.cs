using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Dtos.Messages
{
    public class SendMessageDto
    {
        public int UserID { get; set; }

        public string Message { get; set; }

        public string ServerGroup { get; set; }
    }
}
