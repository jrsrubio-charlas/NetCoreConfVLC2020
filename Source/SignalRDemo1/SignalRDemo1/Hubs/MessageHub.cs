using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRDemo1.Hubs
{
    public class MessageHub : Hub
    {
        private static int users=0;

        //Cuando un usuario se conecta
        public override Task OnConnectedAsync()
        {            
            return Clients.All.SendAsync("users-connected", ++users);
        }

        //Cuando un usuario se desconecta
        public override Task OnDisconnectedAsync(Exception exception)
        {            
            return Clients.All.SendAsync("users-connected", --users);
        }

        public Task BroadcastMessage(string message)
        {
            return Clients.All.SendAsync("new-message", message);
        }
    }
}
