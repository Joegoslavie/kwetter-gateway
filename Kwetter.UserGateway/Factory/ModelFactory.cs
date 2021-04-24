using Kwetter.ServiceLayer.Model;
using Kwetter.UserGateway.VIewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.Factory
{
    public class ModelFactory
    {
        public static AccountViewModel Convert(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return new AccountViewModel
            {
                Id = account.Id,
                Username = account.Username,
                Email = account.Email,
                Profile = ModelFactory.Convert(account.Profile),
            };
        }

        public static ProfileViewModel Convert(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            return new ProfileViewModel
            {
                Username = profile.Username,
                DisplayName = profile.DisplayName,
                d
            };
        }

        public static TweetViewModel Convert(Tweet tweet)
        {
            if (tweet == null)
            {
                throw new ArgumentNullException(nameof(tweet));
            }

            return new TweetViewModel
            {

            };
        }
    }
}
