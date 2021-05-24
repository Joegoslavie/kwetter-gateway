namespace Kwetter.Business.Manager
{
    using Kwetter.DataAccess.Model;
    using Kwetter.DataAccess.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class TweetManager
    {
        private readonly TweetService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetManager"/> class.
        /// </summary>
        public TweetManager(TweetService tweetService)
        {
            // Todo, make analyser.
            this.service = tweetService;
        }

        public async Task<Tweet> Place(int userId, string tweetContent)
        {
            return await this.service.Place(userId, tweetContent).ConfigureAwait(false);
        }

        public void GetTweetsById(int userId)
        {

        }

        public void GetTimeline()
        {

        }
    }
}
