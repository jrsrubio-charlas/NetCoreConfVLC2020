using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SignalRDemo2.PublishToSignalR.Configuration
{
    public class AppSettingsProvider
    {
        private static IConfiguration Config => ConfigProvider.GetConfig();

        public static string MyTwitterUser => Config.GetValue<string>("MyTwitterUser");

        public static string EhConnectionStringToSendTweet => Config.GetValue<string>("EhConnectionStringToSendTweet");
    }
}
