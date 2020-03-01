using System;

namespace SignalRDemo2.TwitterStream.Models
{
    public class TweetSimulatorModel
    {        
        public TweetCreatedBy TweetCreatedBy { get; set; }
        public string TweetText { get; set; }
        public string TweetUrl { get; set; }
        public DateTime TweetCreatedAt { get; set; }
    }

    public class TweetCreatedBy
    {
        public string CreatedBy { get; set; }
        public string ScreenName { get; set; }
    }
}
