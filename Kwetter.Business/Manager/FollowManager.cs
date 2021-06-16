using Kwetter.DataAccess.Model;
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
        private readonly DataAccess.Service.FollowingService followService;
        private readonly ProfileService profileService;
        public FollowManager(DataAccess.Service.FollowingService followService, ProfileService profileService)
        {
            this.followService = followService;
            this.profileService = profileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Profile>> GetFollowers(string username, int page, int amount)
        {
            try
            {
                return await this.followService.GetFollowersByUsername(username, page, amount).ConfigureAwait(false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Profile>> GetFollowing(string username, int page, int amount)
        {
            try
            {
                return await this.followService.GetFollowingByUsername(username, page, amount).ConfigureAwait(false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> ToggleFollow(int id, string username)
        {
            try
            {
                var profile = await this.profileService.GetProfileByUsername(username).ConfigureAwait(false);
                return await this.followService.ToggleFollow(id, username).ConfigureAwait(false);
            }
            catch (Exception) { throw; }
        }
    }
}
