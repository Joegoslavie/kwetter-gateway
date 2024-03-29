﻿namespace Kwetter.Business.Manager
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
        /// Retrieves the account of the passed <paramref name="account"/>. Additionally, based on the extra arguments more data
        /// is retrieved such as tweets, following and followers profiles. 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="includeTweets">Indicates if the tweets need to be retrieved.</param>
        /// <param name="withFollowings">Indicates if the following/follower properties should be retrieved.</param>
        /// <param name="includeBlocked">Indicates if the blocked users should be retrieved.</param>
        /// <returns><see cref="Profile"/> of the passed <see cref="Account"/> parameter.</returns>
        public async Task<Profile> ConstructAccount(
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
                    var tweets = await this.tweetService.GetTweets(account.Id, 1, 50).ConfigureAwait(false);
                    profile.Tweets = tweets.ToList();
                }

                // profile page CEES
                if (withFollowings)
                {
                    var followers = this.followService.GetFollowersByUsername(account.Username, 1, 25);
                    var following = this.followService.GetFollowingByUsername(account.Username, 1, 25);
                    await Task.WhenAll(followers, following).ConfigureAwait(false);

                    profile.Followers = followers.Result.ToList();
                    profile.Following = following.Result.ToList();

                    if (!account.Timeline.Any())
                    {
                        account.Timeline = await this.tweetService.RandomTimeline(1, 30).ConfigureAwait(false);
                    }
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
