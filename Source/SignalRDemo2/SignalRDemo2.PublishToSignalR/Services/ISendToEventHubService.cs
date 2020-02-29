using System.Threading.Tasks;

namespace SignalRDemo2.PublishToSignalR.Services
{
    public interface ISendToEventHubService
    {
        Task SendMessageToEventHub<T>(string ehEntityPath, T model);
    }
}
