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
    using Kwetter.Business.Factory;

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
        /// Initializes a new instance of the <see cref="TweetService"/> class.
        /// </summary>
        /// <param name="settings">Current application settings.</param>
        public TweetService(AppSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Retrieves the tweets created by the passed <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="amount">Amount of tweets to retrieve.</param>
        /// <returns>List of <see cref="Tweet"/>s.</returns>
        public async Task<IEnumerable<Tweet>> GetTweets(int userId, int amount = 150)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.GetTweetsByUserIdAsync(new TweetRequest { UserId = userId, Amount = amount, });
            });

            if (!response.Status)
            {
                throw new Exception(response.Message);
            }

            return response.Tweets.Select(t => TweetFactory.Parse(t));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tweetId"></param>
        /// <returns></returns>
        public async Task<bool> ToggleLike(int tweetId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Places a new tweet in the microservice backend.
        /// </summary>
        /// <param name="userId">Author user id.</param>
        /// <param name="message">Content of the tweet.</param>
        /// <returns></returns>
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
