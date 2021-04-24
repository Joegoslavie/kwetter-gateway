using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Model
{
    public class Tweet
    {
        /// <summary>
        /// Gets or sets the author of the tweet.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the content of the tweet.
        /// </summary>
        public string Content { get;set; }

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
