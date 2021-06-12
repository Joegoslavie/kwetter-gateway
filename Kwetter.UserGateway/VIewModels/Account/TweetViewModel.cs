using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Account
{
    public class TweetViewModel
    {
        public TweetViewModel(Kwetter.DataAccess.Model.Tweet tweet)
        {
            this.Id = tweet.Id;
            this.Content = tweet.Content;
            this.DisplayName = tweet.DisplayName;
            this.Username = tweet.Username;
            this.Liked = tweet.Liked;
            this.AvatarUrl = tweet.AvatarUrl;
            this.CreatedAt = tweet.CreatedAt;
        }

        public TweetViewModel()
        {

        }

        public int Id { get; set; }
        public string Content { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the username that owns the tweet.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the tweet is reported.
        /// </summary>
        public bool Reported { get; set; }

        /// <summary>
        /// Gets or sets the amount of likes this tweet has.
        /// </summary>
        public int Liked { get; set; }
    }
}
