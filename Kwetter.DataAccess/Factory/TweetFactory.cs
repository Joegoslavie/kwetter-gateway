using Kwetter.DataAccess.Model;
using Microservice.TweetGRPCService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.DataAccess.Factory
{
    class TweetFactory
    {
        internal static Tweet Parse(TweetResponseData response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return new Tweet
            {
                Id = response.Id,
                Username = response.Username,
                DisplayName = response.DisplayName,
                AvatarUrl = response.AvatarUri,
                Content = response.Description,
                CreatedAt = new DateTime(response.CreatedAt),
            };
        }
    }
}
