using Kwetter.ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Service
{
    public class TweetService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        public TweetService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<List<Tweet>> GetTweets(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ToggleLike(int tweetId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tweet> Place(int userId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
