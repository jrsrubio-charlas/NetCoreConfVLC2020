using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using SignalRDemo2.CrossCutting.ModelsSettings;

namespace SignalRDemo2.TwitterStream.Services
{
    public class SendToEventHubService : ISendToEventHubService
    {
        private readonly EventHubSettingsModels _ehConfig;
        private EventHubClient eventHubClient;

        public SendToEventHubService(EventHubSettingsModels ehConfig)
        {
            _ehConfig = ehConfig;
        }

        public async Task SendMessageToEventHub<T>(string ehEntityPath, T model)
        {
            var message = JsonConvert.SerializeObject(model);

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(_ehConfig.EhConnectionString)
            {
                EntityPath = ehEntityPath
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }
    }
}

