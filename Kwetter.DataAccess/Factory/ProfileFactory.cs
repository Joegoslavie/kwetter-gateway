namespace Kwetter.DataAccess.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Kwetter.DataAccess.Model;
    using Kwetter.FollowingService;
    using Microservice.ProfileGRPCService;

    /// <summary>
    /// Class for constructing or converting to <see cref="Profile"/> objects.
    /// </summary>
    class ProfileFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static Profile Parse(SingleProfileResponse response)
        {
            if (response == null || response.Profile == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return Parse(response.Profile);
        }

        internal static Profile Parse(ProfileFollowResponse response)
        {
            return new Profile
            {
                UserId = response.UserId,
                Username = response.Username,
                DisplayName = response.DisplayName,
                AvatarUrl = response.AvatarUrl,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static IEnumerable<Profile> Parse(MultipleProfileResponse response)
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
        internal static Profile Parse (ProfileResponse response)
        {
            return new Profile 
            { 
                UserId = response.UserId,
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
