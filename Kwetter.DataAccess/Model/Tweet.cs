namespace Kwetter.DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tweet model.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username that owns the tweet.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the content of the tweet.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the avatar url.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the tweet is reported.
        /// </summary>
        public bool Reported { get; set; }

        /// <summary>
        /// Gets or sets the amount of likes this tweet has.
        /// </summary>
        public int Liked { get; set; }

        /// <summary>
        /// Gets or sets the date the tweet was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

    }
}
