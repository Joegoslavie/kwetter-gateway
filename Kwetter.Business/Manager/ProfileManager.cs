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
        private readonly ProfileService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetManager"/> class.
        /// </summary>
        public ProfileManager(ProfileService profileService)
        {
            // Todo, make analyser.
            this.service = profileService;
        }

        public async Task<Profile> Update(int id, string username, string displayName, string websiteUrl, string description, string location)
        {
            return await this.service.Update(id, username, displayName, websiteUrl, description, location).ConfigureAwait(false);
        }
    }
}
