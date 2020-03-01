using SignalRDemo2.CrossCutting.Models;
using SignalRDemo2.CrossCutting.ModelsSettings;
using SignalRDemo2.TwitterStream.Configuration;
using SignalRDemo2.TwitterStream.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Tweetinvi;

namespace SignalRDemo2.TwitterStream
{
    class Program
    {
        const string EhEntityPath = "newtweet";
        public static bool _isDebug;

        static void Main(string[] args)
        {

#if DEBUG
            _isDebug = true;
#endif
          
            if (_isDebug)
            {
                while (true)
                {
                    Console.WriteLine("Press Y to send new messages");
                    var sendNewMessage = Console.ReadLine();
                    if (sendNewMessage.ToLowerInvariant() == "y")
                    {
                        List<TweetSimulatorModel> newTweetSimulator = new List<TweetSimulatorModel>
                        {
                            new TweetSimulatorModel
                            {
                                TweetCreatedBy =  new TweetCreatedBy {
                                    CreatedBy ="Jose Ramos",
                                    ScreenName = "@jrsrubio" },
                                TweetText = "Aquí estoy, cambiando las demos que YA FUNCIONABAN para la #netcoreconf de Valencia de este sábado...puede salir mal...",
                                TweetUrl = "https://twitter.com/jrsrubio/status/1233112397491122176",
                                TweetCreatedAt = Convert.ToDateTime("27/02/2020 21:25:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="Alberto Rf",
                                    ScreenName = "@ferraces" },
                                TweetText = "Finalmente no podré ir a la @netcoreconf de #Valencia de este finde, así que liberaré la entrada ahora mismo para que si alguien puede ir la aproveche #nosvemosenlaproxima #netcoreconf",
                                TweetUrl = "https://twitter.com/ferraces/status/1232982746030538752",
                                TweetCreatedAt = Convert.ToDateTime("27/02/2020 16:15:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="netcoreconf",
                                    ScreenName = "@netcoreconf" },
                                TweetText = "📣 Ubicación 📣 Un evento como la #NetCoreConf tiene que estar en un sitio acorde y en Valencia tendremos la suerte de hacerlo en el @parccientificuv Para todos los que no vengan de Valencia os dejamos como poder ir en transporte público",
                                TweetUrl = "https://twitter.com/netcoreconf/status/1232228742384246784",
                                TweetCreatedAt = Convert.ToDateTime("25/02/2020 11:45:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="netcoreconf",
                                    ScreenName = "@netcoreconf" },
                                TweetText = "ATENCION!! A todos l@s speakers de la #netcoreconf de Valencia revisar vuestro mail y la carpeta de SPAM que hemos enviado las últimas instrucciones para el evento.  Que no os perdáis lo bueno :) #net #microsoft #azure #valencia",
                                TweetUrl = "https://twitter.com/netcoreconf/status/1231514300180725760",
                                TweetCreatedAt = Convert.ToDateTime("23/02/2020 23:05:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="netcoreconf",
                                    ScreenName = "@netcoreconf" },
                                TweetText = "📣 AGENDA Cambio 📣 Managers para developers by @annalmunim & @AdrianDiaz81 +info: https://bit.ly/35wo1VI #netcoreconf #netcore #Azure #Microsoft #Cloud #Xamarin #net #IA #BigData #IoT",
                                TweetUrl = "https://twitter.com/netcoreconf/status/1230054416767574017",
                                TweetCreatedAt = Convert.ToDateTime("19/02/2020 13:00:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="TOKIOTA",
                                    ScreenName = "@tokiota_IT" },
                                TweetText = "¿Ya conoces la agenda de #netcoreconf en #Valencia para el 29/02/2020? https://netcoreconf.com/valencia.html Si quieres una de las 5 entradas que tenemos, solo tienes que seguirnos en Twitter y escribirnos antes de este viernes en este hilo. Corre que seguro que vuelan!!😉",
                                TweetUrl = "https://twitter.com/tokiota_IT/status/1229365323499016193",
                                TweetCreatedAt = Convert.ToDateTime("17/02/2020 12:21:51")
                            },

                            new TweetSimulatorModel
                            {
                                TweetCreatedBy = new TweetCreatedBy {
                                    CreatedBy ="Jose Ramos",
                                    ScreenName = "@jrsrubio" },
                                TweetText = "Ya tenemos agenda para la #netcoreconf. Estaré (@jrsrubio) en el track 3 a partir de las 16:15 hablando sobre SignalR Core...no quiero hacer muchos spoilers, pero la demo final va a ser muy participativa. Os espero allí....por cierto....aseguraros de que el móvil tenga batería 😉",
                                TweetUrl = "https://twitter.com/jrsrubio/status/1228661663009824769",
                                TweetCreatedAt = Convert.ToDateTime("15/02/2020 13:45:51")
                            }
                        };


                        Services.SendToEventHubService sendToEventHub = new Services.SendToEventHubService(new EventHubSettingsModels 
                        {                            
                            EhConnectionString = ConfigurationManager.ConnectionStrings["EhConnectionString"].ConnectionString
                        });

                        foreach (var tweet in newTweetSimulator)
                        {
                            var newTweet = new TweetModel
                            {
                                CreatedByScreenName = tweet.TweetCreatedBy.ScreenName,
                                CreatedByName = tweet.TweetCreatedBy.CreatedBy,
                                Text = tweet.TweetText,
                                Url = tweet.TweetUrl,
                                CreatedAt = tweet.TweetCreatedAt
                            };

                            Console.WriteLine($"Tweet Date: {newTweet.CreatedAt}");                            
                            Console.WriteLine($"Tweet ScreenName: {newTweet.CreatedByScreenName}");
                            Console.WriteLine($"Tweet User: {newTweet.CreatedByName}");
                            Console.WriteLine($"Tweet Text: {newTweet.Text}");
                            Console.WriteLine($"Tweet URL: {newTweet.Url}");                            
                            Console.WriteLine($"------");

                            sendToEventHub.SendMessageToEventHub<TweetModel>(EhEntityPath, newTweet).GetAwaiter().GetResult();
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        return;
                    }                    
                }
                

            }
            else
            {
                Services.SendToEventHubService sendToEventHub = new Services.SendToEventHubService(new EventHubSettingsModels
                {
                    EhConnectionString = ConfigurationManager.ConnectionStrings["EhConnectionString"].ConnectionString
                });

                var consumerKey = ConfigProvider.ConsumerKey;
                var consumerSecret = ConfigProvider.ConsumerSecret;
                var accessToken = ConfigProvider.AccessToken;
                var accessTokenSecret = ConfigProvider.AccessTokenSecret;

                Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

                var stream = Stream.CreateFilteredStream();
                stream.AddTrack(ConfigProvider.Keyword);
                //stream.AddTweetLanguageFilter(LanguageFilter.Spanish);

                try
                {
                    stream.MatchingTweetReceived += (sender, arguments) =>
                    {
                        Console.WriteLine($"Tweet Date: {arguments.Tweet.CreatedAt}");
                        Console.WriteLine($"Tweet ScreenName: {arguments.Tweet.CreatedBy.ScreenName}");
                        Console.WriteLine($"Tweet User: {arguments.Tweet.CreatedBy.Name}");
                        Console.WriteLine($"Tweet Text: {arguments.Tweet.Text}");
                        Console.WriteLine($"Tweet FullText: {arguments.Tweet.FullText}");
                        Console.WriteLine($"Tweet Language: {arguments.Tweet.Language}");
                        Console.WriteLine($"Tweet URL: {arguments.Tweet.Url}");
                        Console.WriteLine($"Tweet FavoriteCount: {arguments.Tweet.FavoriteCount}");                        
                        Console.WriteLine($"Tweet IsRetweet: {arguments.Tweet.IsRetweet}");
                        Console.WriteLine($"Tweet RetweetCount: {arguments.Tweet.RetweetCount}");
                        Console.WriteLine($"------");

                        var newTweet = new TweetModel
                        {
                            CreatedAt = arguments.Tweet.CreatedAt,
                            Text = arguments.Tweet.Text,
                            FullText = arguments.Tweet.FullText,
                            Url = arguments.Tweet.Url,
                            CreatedByName = arguments.Tweet.CreatedBy.Name,
                            CreatedByScreenName = arguments.Tweet.CreatedBy.ScreenName,
                            FavoriteCount = arguments.Tweet.FavoriteCount,
                            Language = arguments.Tweet.Language.ToString(),
                            IsRetweet = arguments.Tweet.IsRetweet,
                            RetweetCount = arguments.Tweet.RetweetCount                            
                        };

                        sendToEventHub.SendMessageToEventHub<TweetModel>(EhEntityPath, newTweet).GetAwaiter().GetResult();
                    };

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            
                stream.StartStreamMatchingAnyCondition();
            }
        }        
    }
}
