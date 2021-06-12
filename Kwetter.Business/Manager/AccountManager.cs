namespace Kwetter.Business.Manager
{
    using Grpc.Core;
    using Kwetter.DataAccess.Model;
    using Kwetter.DataAccess.Model.Enum;
    using Kwetter.DataAccess.Service;
    using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AccountManager> logger;

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
        public AccountManager(ILogger<AccountManager> logger, ProfileService profileService, TweetService tweetService, FollowingService followingService)
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

                    Task<IEnumerable<Profile>> followerProfiles = this.profileService.GetMultiple(followData[FollowType.Followers]);
                    Task<IEnumerable<Profile>> followingProfiles = this.profileService.GetMultiple(followData[FollowType.Following]);
                    Task<List<Tweet>> timeline = this.tweetService.GetTimeline(followData[FollowType.Following]);
                    // add timeline here
                    //                         account.Timeline = this.tweetService.GetTimeline(account.Id).ConfigureAwait(false);
                    // todo: check if follower/following > 0 to make call
                    await Task.WhenAll(followerProfiles, followingProfiles, timeline).ConfigureAwait(false);

                    profile.Followers = followerProfiles.Result.ToList();
                    profile.Following = followingProfiles.Result.ToList();
                    account.Timeline = timeline.Result;
                }

                if (includeBlocked)
                {
                    // retrieve blocked.
                }

                return profile;
            }
            catch (RpcException ex)
            {
                // From the other side.
                throw new Exception("Exception occurred in microservice", ex);
            }
            catch (Exception ex)
            {
                // This side, that side.. who knows.
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
