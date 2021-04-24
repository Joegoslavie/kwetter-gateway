using Kwetter.Business.Model;
using Kwetter.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Manager
{
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
                account.Profile.Tweets = await this.tweetService.GetTweets(account.Id);

            if (withFollowings)
            {
                var followers = this.profileService.GetProfile(account.Id);
                var following = this.profileService.GetProfile(account.Id);
                await Task.WhenAll(followers, following).ConfigureAwait(false);

                //account.Profile.Followers = followers.Result;
                //account.Profile.Following = following.Result;
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
