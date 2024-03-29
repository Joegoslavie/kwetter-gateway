﻿namespace Kwetter.DataAccess.Service
{
    using Kwetter.DataAccess.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Grpc.Net.Client;
    using Kwetter.DataAccess.Factory;
    using Microservice.TweetGRPCService;

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
        /// <param name="page">User id.</param>
        /// <param name="amount">Amount of tweets to retrieve.</param>
        /// <returns>List of <see cref="Tweet"/>s.</returns>
        public async Task<IEnumerable<Tweet>> GetTweets(int userId, int page, int amount)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.GetTweetsByUserIdAsync(new TweetRequest 
                { 
                    UserId = userId,
                    Page = page,
                    Amount = amount,
                });
            });

            return response.Tweets.Select(t => TweetFactory.Parse(t));
        }

        public async Task<IEnumerable<Tweet>> GetMentions(string username, int page, int amount)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.GetMentionsByUsernameAsync(new TweetRequest
                {
                    Username = username,
                    Page = page,
                    Amount = amount,
                });
            });

            return response.Tweets.Select(t => TweetFactory.Parse(t));
        }

        /// <summary>
        /// Retrieves the tweets created by the passed <paramref name="userId"/>.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="amount">Amount of tweets to retrieve.</param>
        /// <returns>List of <see cref="Tweet"/>s.</returns>
        public async Task<IEnumerable<Tweet>> GetTweets(string username, int page, int amount)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.GetTweetsByUsernameAsync(new TweetRequest 
                { 
                    Username = username, 
                    Page = page,
                    Amount = amount,
                });
            });

            return response.Tweets.Select(t => TweetFactory.Parse(t));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tweetId"></param>
        /// <returns></returns>
        public async Task<bool> ToggleLike(int userId, int tweetId)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.ToggleLikeAsync(new TweetOperationRequest { UserId = userId, TweetId = tweetId, });
            });

            return response.Status;
        }

        public async Task<List<Tweet>> RandomTimeline(int page, int amount)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.GetRandomTimelineAsync(new TweetRequest { Page = page, Amount = amount });
            });

            return response.Tweets.Select(x => TweetFactory.Parse(x)).ToList();
        }

        /// <summary>
        /// Places a new tweet in the microservice backend.
        /// </summary>
        /// <param name="userId">Author user id.</param>
        /// <param name="message">Content of the tweet.</param>
        /// <returns></returns>
        public async Task<Tweet> Place(int userId, string message)
        {
            var response = await this.TweetClientCall(async client =>
            {
                return await client.PlaceTweetAsync(new PlaceTweetRequest { UserId = userId, Content = message, });
            });

            return TweetFactory.Parse(response.Tweets.FirstOrDefault());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tweet>> GetTimeline(List<int> userIds, int page, int amount)
        {
            var response = await this.TweetClientCall(async client =>
            {
                var request = new TweetRequest
                {
                    Page = page,
                    Amount = amount,
                };

                request.UserIds.AddRange(userIds);
                return await client.GetTweetsByUserIdsAsync(request);
            });

            return response.Tweets.Select(t => TweetFactory.Parse(t));
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
