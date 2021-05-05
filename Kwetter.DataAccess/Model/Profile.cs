namespace Kwetter.DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Profile model
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the username of the profile.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the profile display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the profile description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the website Uri.
        /// </summary>
        public string WebsiteUri { get; set; }

        /// <summary>
        /// Gets or sets the location string of the user.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the avatar Uri.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the tweet of the user.
        /// </summary>
        public List<Tweet> Tweets { get; set; } = new List<Tweet>();

        /// <summary>
        /// Gets or sets the followers of the user.
        /// </summary>
        public List<Profile> Following { get; set; } = new List<Profile>();

        /// <summary>
        /// Gets or sets the followers of the user.
        /// </summary>
        public List<Profile> Followers { get; set; } = new List<Profile>();

        /// <summary>
        /// Gets or sets the blocked users.
        /// </summary>
        public List<Profile> Blocked { get; set; } = new List<Profile>();
    }
}
