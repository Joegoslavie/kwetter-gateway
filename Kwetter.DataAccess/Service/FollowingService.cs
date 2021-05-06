﻿namespace Kwetter.DataAccess.Service
{
    using Grpc.Net.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microservice.FollowingGRPCService;
    using Kwetter.DataAccess.Model.Enum;

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
        /// Retrieves the <see cref="FollowType.Following"/> and <see cref="FollowType.Followers"/> properties of the passed
        /// user id. 
        /// </summary>
        /// <param name="userId">User id to retrieve data from.</param>
        /// <returns>Dictionary with type and ids that belong to the type.</returns>
        public async Task<IDictionary<FollowType, IEnumerable<int>>> FetchIds(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the <see cref="FollowType.Following"/> and <see cref="FollowType.Followers"/> properties of the passed
        /// user id. 
        /// </summary>
        /// <param name="username">Username to retrieve data from.</param>
        /// <returns>Dictionary with type and ids that belong to the type.</returns>
        public async Task<IDictionary<FollowType, IEnumerable<int>>> FetchIds(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Toggles a follow on the <paramref name="followId"/> in the microservice backend. If the user is already following the
        /// user the <paramref name="followId"/> will be unfollowed. Or else the <paramref name="followId"/> will be followed.
        /// </summary>
        /// <param name="userId">Current user id.</param>
        /// <param name="followId">Id to toggle following on.</param>
        /// <returns>Bool indicating if the user has started following or unfollowed the user. Where <see cref="true"/> means that a new follow record was created
        /// and <see cref="false"/> that an unfollow operation was performed.</returns>
        public async Task<bool> ToggleFollow(int userId, int followId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Toggles a block on the <paramref name="blockId"/> in the microservice backend. If the user is already blocked the
        /// user <paramref name="blockId"/> will be unblocked. Or else the <paramref name="blockId"/> will be blocked.
        /// </summary>
        /// <param name="userId">Current user id.</param>
        /// <param name="blockId">Id to toggle block on.</param>
        /// <returns>Bool indicating if the user has started following or unfollowed the user. Where <see cref="true"/> means that a new block record was created
        /// and <see cref="false"/> that an unblock operation was performed.</returns>
        public async Task<bool> ToggleBlock(int userId, int blockId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a service call.
        /// </summary>
        /// <typeparam name="TParsedResponse">Parsed response.</typeparam>
        /// <param name="responseHandler">Handler.</param>
        /// <returns></returns>
        private async Task<TParsedResponse> FollowClientCall<TParsedResponse>(Func<FollowingGRPCService.FollowingGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.settings.TweetServiceUrl))
            {
                var client = new FollowingGRPCService.FollowingGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}