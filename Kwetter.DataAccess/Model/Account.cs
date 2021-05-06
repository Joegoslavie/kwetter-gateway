namespace Kwetter.DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Account model.
    /// </summary>
    [DebuggerDisplay("{Username} | id: {Id}")]
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
    }
}
