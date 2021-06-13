using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Account
{
    public class AccountViewModel
    {
        public AccountViewModel(DataAccess.Model.Account account)
        {
            this.Id = account.Id;
            this.Username = account.Username;
            this.Email = account.Email;
            this.Token = account.Token;
            this.Profile = new ProfileViewModel(account.Profile);
            this.Timeline = account.Timeline.Select(x => new TweetViewModel(x)).ToList();
        }

        public AccountViewModel()
        {

        }

        /// <summary>
        /// Gets or sets the id of the user. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProfileViewModel Profile { get; set; }

        /// <summary>
        /// Gets or sets the timeline tweets of the user.
        /// </summary>
        public List<TweetViewModel> Timeline { get; set; } = new List<TweetViewModel>();


    }
}
