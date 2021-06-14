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
            // Todo: handle different exceptions
            this.service = tweetService;
            this.followService = followingService;
        }

        public async Task<Tweet> Place(int userId, string tweetContent)
        {
            try
            {
                return await this.service.Place(userId, tweetContent).ConfigureAwait(false);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> ToggleLike(int userId, int tweetId)
        {
            try
            {
                return await this.service.ToggleLike(userId, tweetId).ConfigureAwait(false);
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Tweet>> GetTweetsById(int userId, int page, int amount)
        {
            try
            {
                IEnumerable<Tweet> tweets = await this.service.GetTweets(userId, page, amount).ConfigureAwait(false);
                return tweets.ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Tweet>> GetTweetsByUsername(string username, int page, int amount)
        {
            try {
                IEnumerable<Tweet> tweets = await this.service.GetTweets(username, page, amount).ConfigureAwait(false);
                return tweets.ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Tweet>> GetTimelineById(int userId, int page, int amount)
        {
            try
            {
                Dictionary<FollowType, List<int>> followingIds = await this.followService.FetchIds(userId).ConfigureAwait(false);
                IEnumerable<Tweet> tweets = await this.service.GetTimeline(followingIds[FollowType.Following], page, amount).ConfigureAwait(false);
                return tweets.ToList();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Tweet>> GetRandomTimeline(int page, int amount)
        {
            try
            {
                var tweets = await this.service.RandomTimeline(page, amount).ConfigureAwait(false);
                return tweets.ToList();
            }
            catch (Exception) { throw; }
        }
    }
}
