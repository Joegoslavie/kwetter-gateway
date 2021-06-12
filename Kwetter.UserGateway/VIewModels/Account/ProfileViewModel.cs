using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Account
{
    public class ProfileViewModel
    {
        public ProfileViewModel(DataAccess.Model.Profile profile)
        {
            this.Username = profile.Username;
            this.DisplayName = profile.DisplayName;
            this.WebsiteUrl = profile.WebsiteUri ?? string.Empty;
            this.Location = profile.Location ?? string.Empty;
            this.Description = profile.Description;
            this.AvatarUrl = profile.AvatarUrl;
            this.Followers = profile.Followers.Select(x => new ProfileViewModel(x)).ToList();
            this.Following = profile.Following.Select(x => new ProfileViewModel(x)).ToList();
            this.Tweets = profile.Tweets.Select(x => new TweetViewModel(x)).ToList();
        }

        public ProfileViewModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TweetViewModel> Tweets { get; set; } = new List<TweetViewModel>();

        /// <summary>
        /// 
        /// </summary>
        public List<ProfileViewModel> Following { get; set; } = new List<ProfileViewModel>();

        /// <summary>
        /// 
        /// </summary>
        public List<ProfileViewModel> Followers { get; set; } = new List<ProfileViewModel>();

    }
}
