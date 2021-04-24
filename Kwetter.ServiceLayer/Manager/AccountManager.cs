namespace Kwetter.Business.Manager
{
    using Kwetter.Business.Model;
    using Kwetter.Business.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Account manager class.
    /// </summary>
    public class AccountManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ProfileService profileService;

        /// <summary>
        /// 
        /// </summary>
        private readonly TweetService tweetService;

        /// <summary>
        /// 
        /// </summary>
        private readonly FollowingService followService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> class.
        /// </summary>
        /// <param name="profileService"></param>
        /// <param name="tweetService"></param>
        /// <param name="followingService"></param>
        public AccountManager(ProfileService profileService, TweetService tweetService, FollowingService followingService)
        {
            this.profileService = profileService;
            this.tweetService = tweetService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="includeTweets"></param>
        /// <param name="includeFollowers"></param>
        /// <param name="includeFollowing"></param>
        /// <param name="includeBlocked"></param>
        /// <returns></returns>
        public async Task<Profile> GetProfile(
            Account account,
            bool includeTweets = false,
            bool withFollowings = false,
            bool includeBlocked = false)
        {
            account.Profile = await this.profileService.GetProfile(account.Id).ConfigureAwait(false);

            if (includeTweets)
            {
                var tweets = await this.tweetService.GetTweets(account.Id).ConfigureAwait(false);
                account.Profile.Tweets = tweets.ToList();
            }

            if (withFollowings)
            {
                var followerIds = this.followService.LookupFollowersIds(account.Id);
                var followingIds = this.followService.LookupFollowersIds(account.Id);
                await Task.WhenAll(followerIds, followingIds).ConfigureAwait(false);

                var followerProfiles = this.profileService.GetMultiple(followerIds.Result);
                var followingProfiles = this.profileService.GetMultiple(followingIds.Result);
                await Task.WhenAll(followerProfiles, followingProfiles).ConfigureAwait(false);

                account.Profile.Followers = followerProfiles.Result.ToList();
                account.Profile.Following = followingProfiles.Result.ToList();
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="includeTweets"></param>
        /// <param name="includeFollowers"></param>
        /// <param name="includeFollowing"></param>
        /// <param name="includeBlocked"></param>
        /// <returns></returns>
        public async Task<Profile> GetAccounts(
            IEnumerable<int> userIds,
            bool includeTweets = false,
            bool includeFollowers = false,
            bool includeFollowing = false,
            bool includeBlocked = false)
        {
            throw new NotImplementedException();
        }
    }
}
