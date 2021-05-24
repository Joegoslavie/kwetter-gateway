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
        private readonly FollowingService service;
        public FollowManager(FollowingService service)
        {
            this.service = service;
        }

        public async Task<bool> ToggleFollow(int id, int followId)
        {
            return await this.service.ToggleFollow(id, followId).ConfigureAwait(false);
        }
    }
}
