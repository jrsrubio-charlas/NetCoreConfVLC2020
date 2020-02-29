using Microsoft.Extensions.Configuration;

namespace SignalRDemo2.SendTweet.Configuration
{
    public class AppSettingsProvider
    {
        private static IConfiguration Config => ConfigProvider.GetConfig();

        public static string ConsumerKey => Config.GetValue<string>("ConsumerKey");
        public static string ConsumerSecret => Config.GetValue<string>("ConsumerSecret");
        public static string AccessToken => Config.GetValue<string>("AccessToken");
        public static string AccessTokenSecret => Config.GetValue<string>("AccessTokenSecret");
    }
}
