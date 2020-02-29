using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using SignalRDemo2.CrossCutting.Models;
using SignalRDemo2.CrossCutting.ModelsSettings;
using SignalRDemo2.PublishToSignalR.Configuration;

namespace SignalRDemo2.PublishToSignalR
{    
    public static class Function1
    {

        const string EhEntityPath = "newtweettosend";

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "newtweet")] SignalRConnectionInfo connectionInfo)
        {

            return connectionInfo;
        }
                
        [FunctionName("newtweet")]
        public static async Task PublishNewTweetOnSignalR(
            [EventHubTrigger("newtweet", Connection = "EhConnectionString")] EventData[] events,
            [SignalR(HubName = "newtweet")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var myTwitterUser = AppSettingsProvider.MyTwitterUser;

                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    
                    var newTweetReceivedExpanded = JsonConvert.DeserializeObject<TweetExtendedModel>(messageBody);

                    newTweetReceivedExpanded.MentionedMe = newTweetReceivedExpanded.Text.ToLowerInvariant().Contains(myTwitterUser) ? true : false;

                    await signalRMessages.AddAsync(
                        new SignalRMessage
                        {
                            //Para enviar el mensaje a un grupo determinado
                            //GroupName = "groupName",
                            //Para enviar el mensaje a un usuario determinado
                            //UserId = "userId",
                            //Para enviar el mensaje a una connectionId determinada
                            //ConnectionId = "connectionId",                            

                            Target = "newTweet",
                            Arguments = new[] { JsonConvert.SerializeObject(newTweetReceivedExpanded) }
                        });

                    if (newTweetReceivedExpanded.MentionedMe)
                    {
                        Services.SendToEventHubService sendToEventHub = new Services.SendToEventHubService(new EventHubSettingsModels
                        {
                            EhConnectionString = AppSettingsProvider.EhConnectionStringToSendTweet
                        });

                        sendToEventHub.SendMessageToEventHub<TweetModel>(EhEntityPath, newTweetReceivedExpanded).GetAwaiter().GetResult();
                    }

                    await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
