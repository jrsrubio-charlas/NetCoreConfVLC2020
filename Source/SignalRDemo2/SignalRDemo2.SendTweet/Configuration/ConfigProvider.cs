using System.Configuration;

namespace SignalRDemo2.SendTweet.Configuration
{
    public class ConfigProvider
    {
        public static string ConsumerKey => ConfigurationManager.AppSettings["consumerKey"];
        public static string ConsumerSecret => ConfigurationManager.AppSettings["consumerSecret"];
        public static string AccessToken => ConfigurationManager.AppSettings["accessToken"];
        public static string AccessTokenSecret => ConfigurationManager.AppSettings["accessTokenSecret"];
    }
}
