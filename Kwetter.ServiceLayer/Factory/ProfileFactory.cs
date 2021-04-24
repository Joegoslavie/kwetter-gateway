using Kwetter.Business.Model;
using Microservice.ProfileGRPCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Factory
{
    internal class ProfileFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Profile Parse(SingleProfileResponse response)
        {
            if (response == null || response.Profile == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return Parse(response.Profile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static IEnumerable<Profile> Parse(MultipleProfileResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return response.Profiles.Select(x => Parse(x));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Profile Parse (ProfileResponse response)
        {
            return new Profile 
            { 
                Username = response.Username,
                DisplayName = response.DisplayName,
                Description = response.Description,
                AvatarUrl = response.AvatarUri,
                WebsiteUri = response.WebsiteUri,
                Location = response.Location,
            };
        }
    }
}
