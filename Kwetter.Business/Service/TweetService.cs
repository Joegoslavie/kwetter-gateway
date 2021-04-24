namespace Kwetter.Business.Service
{
    using Kwetter.Business.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microservice.TweetGRPCService;
    using Grpc.Net.Client;

    /// <summary>
    /// Tweet service.
    /// </summary>
    public class TweetService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public TweetService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<Tweet>> GetTweets(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ToggleLike(int tweetId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tweet> Place(int userId, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a service call.
        /// </summary>
        /// <typeparam name="TParsedResponse">Parsed response.</typeparam>
        /// <param name="responseHandler">Handler.</param>
        /// <returns></returns>
        private async Task<TParsedResponse> TweetClientCall<TParsedResponse>(Func<TweetGRPCService.TweetGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.settings.TweetServiceUrl))
            {
                var client = new TweetGRPCService.TweetGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
