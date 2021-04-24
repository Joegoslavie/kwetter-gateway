using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Service
{
    public class FollowingService
    {
        private readonly AppSettings settings;

        public FollowingService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<int>> LookupFollowers()
        {
            return null;
        }

        public async Task<IEnumerable<int>> LookupFollowing()
        {
            return null;
        }

        public async Task<bool> ToggleFollow(int userId, int followId)
        {
            return null;
        }
    }
}
