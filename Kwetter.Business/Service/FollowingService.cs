namespace Kwetter.Business.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Following service.
    /// </summary>
    public class FollowingService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private readonly AppSettings settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public FollowingService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<int>> LookupFollowersIds(int userId)
        {
            return null;
        }

        public async Task<IEnumerable<int>> LookupFollowingIds(int userId)
        {
            return null;
        }

        public async Task<bool> ToggleFollow(int userId, int followId)
        {
            return false;
        }

        public async Task<bool> ToggleBlock(int userId, int blockId)
        {
            return false;
        }
    }
}
