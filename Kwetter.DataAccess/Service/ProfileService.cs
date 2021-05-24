namespace Kwetter.DataAccess.Service
{
    using Grpc.Net.Client;
    using Kwetter.DataAccess.Exceptions;
    using Kwetter.DataAccess.Factory;
    using Kwetter.DataAccess.Model;
    using Microservice.ProfileGRPCService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Profile service.
    /// </summary>
    public class ProfileService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService"/> class.
        /// </summary>
        /// <param name="settings">Current application settings.</param>
        public ProfileService(AppSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="displayName"></param>
        /// <param name="websiteUrl"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<Profile> Update(int id, string username, string displayName, string websiteUrl, string description, string location)
        {
            var response = await this.ProfileClientCall(async client =>
            {
                var request = new UpdateProfileRequest
                {
                    UserId = id,
                    Username = username,
                    DisplayName = displayName ?? string.Empty,
                    WebsiteUri = websiteUrl ?? string.Empty,
                    Description = description ?? string.Empty,
                    Location = location ?? string.Empty,
                    AvatarUri = "default.jpg",
                };
                return await client.UpdateProfileAsync(request);
            });

            if (!response.Status)
            {
                throw new ProfileException(response.Message);
            }

            return ProfileFactory.Parse(response);
        }

        /// <summary>
        /// Gets the profile related to the user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns><see cref="Profile"/>.</returns>
        public async Task<Profile> GetProfile(int userId)
        {
            var response = await this.ProfileClientCall(async client =>
            {
                return await client.GetProfileByIdAsync(new ProfileRequest { UserId = userId });
            });

            if (!response.Status)
            {
                throw new ProfileException(response.Message);
            }

            return ProfileFactory.Parse(response);
        }

        /// <summary>
        /// Gets the profile related to the username.
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns><see cref="Profile"/>.</returns>
        public async Task<Profile> GetProfileByUsername(string username)
        {
            var response = await this.ProfileClientCall(async client =>
            {
                return await client.GetProfileByUsernameAsync(new ProfileRequest { Username = username });
            });

            if (!response.Status)
            {
                throw new ProfileException(response.Message);
            }

            return ProfileFactory.Parse(response);
        }

        /// <summary>
        /// Gets the profiles associated with the given ids.
        /// </summary>
        /// <param name="userIds">List of user ids.</param>
        /// <returns>List of <see cref="Profile"/>.</returns>
        public async Task<IEnumerable<Profile>> GetMultiple(IEnumerable<int> userIds)
        {
            var response = await this.ProfileClientCall(async client =>
            {
                var request = new ProfileRequest();
                request.UserIds.AddRange(userIds);

                return await client.GetMultipleByIdAsync(request);
            });

            if (!response.Status)
            {
                throw new ProfileException(response.Message);
            }

            return ProfileFactory.Parse(response);
        }

        /// <summary>
        /// Create a service call.
        /// </summary>
        /// <typeparam name="TParsedResponse">Parsed response.</typeparam>
        /// <param name="responseHandler">Handler.</param>
        /// <returns></returns>
        private async Task<TParsedResponse> ProfileClientCall<TParsedResponse>(Func<ProfileGRPCService.ProfileGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.settings.ProfileServiceUrl))
            {
                var client = new ProfileGRPCService.ProfileGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
