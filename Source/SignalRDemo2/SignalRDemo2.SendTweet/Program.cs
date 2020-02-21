using SignalRDemo2.SendTweet.Configuration;
using System;
using Tweetinvi;

namespace SignalRDemo2.SendTweet
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerKey = ConfigProvider.ConsumerKey;
            var consumerSecret = ConfigProvider.ConsumerSecret;
            var accessToken = ConfigProvider.AccessToken;
            var accessTokenSecret = ConfigProvider.AccessTokenSecret;

            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            var user = User.GetAuthenticatedUser();

            var tweet = Tweet.PublishTweet("Hello from tweetinvi");

            var timelineTweets = Timeline.GetUserTimeline(user, 5);

            foreach (var timelineTweet in timelineTweets)
            {
                Console.WriteLine(timelineTweet);
                Console.WriteLine("***********");
            }

            Console.WriteLine();
        }
    }
}
