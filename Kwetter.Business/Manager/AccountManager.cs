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
    /// Account manager class.
    /// </summary>
    public class AccountManager
    {
        /// <summary>
        /// Profile service instance.
        /// </summary>
        private readonly ProfileService profileService;

        /// <summary>
        /// Tweet service instance.
        /// </summary>
        private readonly TweetService tweetService;

        /// <summary>
        /// Follow service instance.
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
            this.followService = followingService;
        }

        /// <summary>
        /// Retrieves the profile of the passed <paramref name="account"/>. Additionally, based on the extra arguments more data
        /// is retrieved such as tweets, following and followers profiles. 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="includeTweets">Indicates if the tweets need to be retrieved.</param>
        /// <param name="withFollowings">Indicates if the following/follower properties should be retrieved.</param>
        /// <param name="includeBlocked">Indicates if the blocked users should be retrieved.</param>
        /// <returns><see cref="Profile"/> of the passed <see cref="Account"/> parameter.</returns>
        public async Task<Profile> GetProfile(
            Account account,
            bool includeTweets = false,
            bool withFollowings = false,
            bool includeBlocked = false)
        {
            try
            {
                var profile = await this.profileService.GetProfile(account.Id).ConfigureAwait(false);

                if (includeTweets)
                {
                    var tweets = await this.tweetService.GetTweets(account.Id).ConfigureAwait(false);
                    profile.Tweets = tweets.ToList();
                }

                // profile page CEES
                if (withFollowings)
                {
                    var followData = await this.followService.FetchIds(account.Id).ConfigureAwait(false);

                    var followerProfiles = this.profileService.GetMultiple(followData[FollowType.Followers]);
                    var followingProfiles = this.profileService.GetMultiple(followData[FollowType.Following]);
                    await Task.WhenAll(followerProfiles, followingProfiles).ConfigureAwait(false);

                    profile.Followers = followerProfiles.Result.ToList();
                    profile.Following = followingProfiles.Result.ToList();
                }

                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
