using System;

namespace SignalRDemo2.CrossCutting.Models
{
    public class TweetModel
    {
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string FullText { get; set; }
        public string Url { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByScreenName { get; set; }
        public int FavoriteCount { get; set; } 
        public string Language { get; set; }
        public bool IsRetweet { get; set; }
        public int RetweetCount { get; set; }              
    }
}