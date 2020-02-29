using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SignalRDemo2.CrossCutting.Models;
using SignalRDemo2.SendTweet.Configuration;
using Tweetinvi;

namespace SignalRDemo2.SendTweet
{
    public static class Function1
    {
        [FunctionName("SendTweet")]
        public static async Task Run(
            [EventHubTrigger("newtweettosend", Connection = "EhConnectionString")] EventData[] events, ILogger log)
        {
            log.LogInformation("New tweet to send");

            foreach (EventData eventData in events)
            {
                try
                {
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                    var newTweetReceivedExpanded = JsonConvert.DeserializeObject<TweetExtendedModel>(messageBody);


                    var consumerKey = AppSettingsProvider.ConsumerKey;
                    var consumerSecret = AppSettingsProvider.ConsumerSecret;
                    var accessToken = AppSettingsProvider.AccessToken;
                    var accessTokenSecret = AppSettingsProvider.AccessTokenSecret;

                    Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

                    var tweetToSend = $"Gracias @{newTweetReceivedExpanded.CreatedByScreenName}. Por si te interesa, te dejo el repo de mi charla por aquí: https://github.com/jrsrubio-charlas/NetCoreConfVLC2020";

                    log.LogInformation($"Tweet to send: {tweetToSend}");

                    var tweet = Tweet.PublishTweet(tweetToSend);

                    log.LogInformation($"Tweet send correctly");

                    await Task.Yield();
                }
                catch (Exception e)
                {
                    log.LogInformation($"Error: {e.InnerException}");
                }
            }

        }
    }
}
