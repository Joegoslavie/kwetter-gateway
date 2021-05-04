namespace Kwetter.DataAccess.Factory
{
    using Kwetter.DataAccess.Model;
    using Microservice.TweetGRPCService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
