using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Dtos.Messages;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessageToGroup(SendMessageDto messageData)
        {
            return Clients
                .Group(messageData.ServerGroup)
                .SendAsync("ReceiveMessage",
                messageData.UserID,
                messageData.Message);
        }
    }
}
