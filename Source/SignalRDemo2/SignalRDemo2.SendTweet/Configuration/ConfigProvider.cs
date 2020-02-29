using Microsoft.Extensions.Configuration;
using System;

namespace SignalRDemo2.SendTweet.Configuration
{
    public class ConfigProvider
    {
        private static IConfiguration Config;

        public static IConfiguration GetConfig()
        {
            if (Config == null)
            {
                Config = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
            }

            return Config;
        }
    }
}
