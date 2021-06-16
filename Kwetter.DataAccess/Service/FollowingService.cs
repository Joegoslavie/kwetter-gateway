namespace Kwetter.DataAccess.Service
{
    using Grpc.Net.Client;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Kwetter.DataAccess.Model;
    using Kwetter.FollowingService;
    using System.Linq;
    using Kwetter.DataAccess.Factory;

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
        /// Initializes a new instance of the <see cref="FollowingService"/> class.
        /// </summary>
        /// <param name="settings">Current application settings.</param>
        public FollowingService(AppSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Get basic profiles of followers of the username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Profile>> GetFollowersByUsername(string username, int page, int amount)
        {
            var response = await this.FollowClientCall(async client =>
            {
                return await client.GetFollowersByUsernameAsync(new FollowInfoRequest
                {
                    Username = username,
                    Page = page,
                    Amount = amount,
                });
            });

            return response.Profiles.Select(x => ProfileFactory.Parse(x)).ToList();
        }

        /// <summary>
        /// Get basic profiles of following of the username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<List<Profile>> GetFollowingByUsername(string username, int page, int amount)
        {
            var response = await this.FollowClientCall(async client =>
            {
                return await client.GetFollowingByUsernameAsync(new FollowInfoRequest
                {
                    Username = username,
                    Page = page,
                    Amount = amount,
                });
            });

            return response.Profiles.Select(x => ProfileFactory.Parse(x)).ToList();
        }

        /// <summary>
        /// Toggles a follow on the <paramref name="followId"/> in the microservice backend. If the user is already following the
        /// user the <paramref name="followId"/> will be unfollowed. Or else the <paramref name="followId"/> will be followed.
        /// </summary>
        /// <param name="userId">Current user id.</param>
        /// <param name="username">username to toggle following on.</param>
        /// <returns>Bool indicating if the user has started following or unfollowed the user. Where <see cref="true"/> means that a new follow record was created
        /// and <see cref="false"/> that an unfollow operation was performed.</returns>
        public async Task<bool> ToggleFollow(int userId, string username)
        {
            var response = await this.FollowClientCall(async client =>
            {
                return await client.ToggleFollowAsync(new ToggleFollowRequest 
                {
                    UserId = userId, 
                    Username = username,
                });
            });

            return response.Status;
        }

        /// <summary>
        /// Toggles a block on the <paramref name="blockId"/> in the microservice backend. If the user is already blocked the
        /// user <paramref name="blockId"/> will be unblocked. Or else the <paramref name="blockId"/> will be blocked.
        /// </summary>
        /// <param name="userId">Current user id.</param>
        /// <param name="username">username to toggle block on.</param>
        /// <returns>Bool indicating if the user has started following or unfollowed the user. Where <see cref="true"/> means that a new block record was created
        /// and <see cref="false"/> that an unblock operation was performed.</returns>
        public async Task<bool> ToggleBlock(int userId, string username)
        {
            var response = await this.FollowClientCall(async client =>
            {
                return await client.ToggleBlockAsync(new ToggleBlockRequest 
                { 
                    UserId = userId, 
                    Username = username 
                });
            });

            return response.Status;
        }

        /// <summary>
        /// Create a service call.
        /// </summary>
        /// <typeparam name="TParsedResponse">Parsed response.</typeparam>
        /// <param name="responseHandler">Handler.</param>
        /// <returns></returns>
        private async Task<TParsedResponse> FollowClientCall<TParsedResponse>(Func<FollowGRPCService.FollowGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.settings.FollowService))
            {
                var client = new FollowGRPCService.FollowGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
