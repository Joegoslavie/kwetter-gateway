using Kwetter.DataAccess.Model;
using Kwetter.DataAccess.Model.Enum;
using Kwetter.DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Manager
{
    public class FollowManager
    {
        private readonly FollowingService followService;
        private readonly ProfileService profileService;
        public FollowManager(FollowingService followService, ProfileService profileService)
        {
            this.followService = followService;
            this.profileService = profileService;
        }

        public async Task<Profile> FullProfile(int userId)
        {
            var followData = await this.followService.FetchIds(userId).ConfigureAwait(false);

            // tasks.
            var ownerProfile = this.profileService.GetProfile(userId);
            var followingProfiles = this.profileService.GetMultiple(followData[FollowType.Followers]);
            var followerProfiles = this.profileService.GetMultiple(followData[FollowType.Followers]);

            await Task.WhenAll(ownerProfile, followerProfiles, followingProfiles).ConfigureAwait(false);

            var profile = ownerProfile.Result;
            profile.Followers = followerProfiles.Result.ToList();
            profile.Following = followingProfiles.Result.ToList();

            return profile;
        }

        public async Task<bool> ToggleFollow(int id, int followId)
        {
            return await this.followService.ToggleFollow(id, followId).ConfigureAwait(false);
        }
    }
}
