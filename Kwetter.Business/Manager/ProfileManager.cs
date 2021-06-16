using Kwetter.DataAccess.Model;
using Kwetter.DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Manager
{
    public class ProfileManager
    {
        private readonly ProfileService profileService;
        private readonly TweetService tweetService;
        private readonly DataAccess.Service.FollowingService followService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetManager"/> class.
        /// </summary>
        public ProfileManager(ProfileService profileService, TweetService tweetService, DataAccess.Service.FollowingService followService)
        {
            // Todo, make analyser.
            this.profileService = profileService;
            this.tweetService = tweetService;
            this.followService = followService;
        }

        public async Task<List<Profile>> Get(List<int> userIds)
        {
            var profiles = await this.profileService.GetMultiple(userIds).ConfigureAwait(false);
            return profiles.ToList();
        }

        public async Task<Profile> Get(int userId)
        {
            return await this.profileService.GetProfile(userId).ConfigureAwait(false);
        }

        public async Task<Profile> Get(string username)
        {
            var profile = await this.profileService.GetProfileByUsername(username).ConfigureAwait(false);
            var tweetReq = this.tweetService.GetTweets(username, 1, 50);
            //await Task.WhenAll(tweetReq, followers, following).ConfigureAwait(false);

            profile.Tweets = tweetReq.Result.ToList().OrderByDescending(x => x.CreatedAt).ToList();
            //profile.Following = following.Result.ToList();
            //profile.Followers = followers.Result.ToList();

            return profile;
        }

        public async Task<Profile> Update(int id, string username, string displayName, string websiteUrl, string description, string location)
        {
            return await this.profileService.Update(id, username, displayName, websiteUrl, description, location).ConfigureAwait(false);
        }
    }
}
