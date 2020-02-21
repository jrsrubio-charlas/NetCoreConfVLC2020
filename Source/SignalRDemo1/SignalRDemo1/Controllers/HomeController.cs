using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo1.Hubs;

namespace SignalRDemo1.Controllers
{
    public class HomeController : Controller
    {
        IHubContext<MessageHub> context;
        public HomeController(IHubContext<MessageHub> hub)
        {
            context = hub;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Para habilitar el poder enviar un mensaje desde la misma vista
        public IActionResult Message()
        {
            return View();
        }
        
        
        public void AddMessage(string message)
        {
            //Para enviar el mensaje a todo el mundo
            context.Clients.All.SendAsync("new-message", message);
        
            //Para enviar el mensaje a un grupo determinado
            //context.Clients.Group("groupName").SendAsync("new-message", message);
        
            //Para enviar el mensaje a un usuario determinado
            //context.Clients.User("userId").SendAsync("new-message", message);
        
            //Para enviar el mensaje a una connectionId determinada
            //context.Clients.Client("connectionId").SendAsync("new-message", message);
        
            //Para enviar el mensaje a todos, menos a la/s connectionId indicadas
            //context.Clients.AllExcept("excludedConnectionId").SendAsync("new-message", message);                    
        }
    }
}
