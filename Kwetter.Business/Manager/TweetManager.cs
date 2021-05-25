namespace Kwetter.Business.Manager
{
    using Kwetter.DataAccess.Model;
    using Kwetter.DataAccess.Model.Enum;
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

        private readonly FollowingService followService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetManager"/> class.
        /// </summary>
        public TweetManager(TweetService tweetService, FollowingService followingService)
        {
            // Todo, make analyser.
            this.service = tweetService;
            this.followService = followingService;
        }

        public async Task<Tweet> Place(int userId, string tweetContent)
        {
            return await this.service.Place(userId, tweetContent).ConfigureAwait(false);
        }

        public async Task<bool> ToggleLike(int userId, int tweetId)
        {
            return await this.service.ToggleLike(userId, tweetId).ConfigureAwait(false);
        }

        public async Task<List<Tweet>> GetTweetsById(int userId)
        {
            var tweets = await this.service.GetTweets(userId).ConfigureAwait(false);
            return tweets.ToList();
        }

        public async Task<List<Tweet>> GetTweetsByUsername(string username)
        {
            var tweets = await this.service.GetTweets(username).ConfigureAwait(false);
            return tweets.ToList();
        }

        public async Task<List<Tweet>> GetTimeline(int userId)
        {
            var followingIds = await this.followService.FetchIds(userId).ConfigureAwait(false);
            var tweets = await this.service.GetTimeline(followingIds[FollowType.Following]).ConfigureAwait(false);
            return tweets.ToList();
        }
    }
}
