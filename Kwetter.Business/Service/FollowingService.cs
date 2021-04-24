namespace Kwetter.Business.Service
{
    using Grpc.Net.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microservice.FollowingGRPCService;

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
