using Kwetter.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Service
{
    public class ProfileService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        public ProfileService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<Profile> GetProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> GetMultiple(IEnumerable<int> userIds)
        {
            throw new NotImplementedException();
        }
    }
}
