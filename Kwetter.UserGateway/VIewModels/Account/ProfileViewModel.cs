using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Account
{
    public class ProfileViewModel
    {
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
