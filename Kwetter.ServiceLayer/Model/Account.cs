namespace Kwetter.Business.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Account model.
    /// </summary>
    public class Account
    {
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
        /// Gets or sets the profile.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets the timeline tweets of the user.
        /// </summary>
        public List<Tweet> Timeline { get; set; } = new List<Tweet>();

        /// <summary>
        /// Gets or sets the tweets the user is mentioned in.
        /// </summary>
        public List<Tweet> Mentions { get; set; } = new List<Tweet>();
    }
}
