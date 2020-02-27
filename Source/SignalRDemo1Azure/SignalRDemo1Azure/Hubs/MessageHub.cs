using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRDemo1Azure.Hubs
{
    public class MessageHub : Hub
    {
        private static int users = 0;

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

        //Publicar pregunta
        public Task BroadcastQuestion(string message)
        {
            return Clients.All.SendAsync("new-question", message);
        }

        //Publicar respuestas
        public void BroadcastAnswer(string message)
        {            
            Clients.Caller.SendAsync("new-answer", $"Gracias por participar 👏👏👏👏 -- {Context.ConnectionId} - {message}");
            Clients.Others.SendAsync("new-answer", $"{Context.ConnectionId} - {message}");
            //return Clients.All.SendAsync("new-answer", $"{Context.ConnectionId} - {message}");
        }
    }
}
