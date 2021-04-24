namespace Kwetter.Business.Service
{
    using Kwetter.Business.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tweet service.
    /// </summary>
    public class TweetService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public TweetService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<Tweet>> GetTweets(int userId)
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
