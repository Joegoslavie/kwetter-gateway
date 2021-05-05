namespace Kwetter.DataAccess
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for retrieving global application settings, possible to override keys for different environments or test purposes.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Configuration class.
        /// </summary>
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="config">IConfiguration instance.</param>
        public AppSettings(IConfiguration config)
        {
            this.config = config;
        }

        #region Properties

        /// <summary>
        /// Gets the authentication service endpoint url from the configuration.
        /// </summary>
        public string AuthenticationServiceUrl => this.config.GetValue<string>(this.AuthServiceKey);

        /// <summary>
        /// Gets the profile service endpoint url from the configuration. 
        /// </summary>
        public string ProfileServiceUrl => this.config.GetValue<string>(this.ProfileServiceKey);

        /// <summary>
        /// Gets the tweet service endpoint url from the configuration.
        /// </summary>
        public string TweetServiceUrl => this.config.GetValue<string>(this.TweetServiceKey);

        #endregion

        #region keys

        /// <summary>
        /// Sets the app key to use for the authentication service.
        /// </summary>
        public string AuthServiceKey { private get; set; } = "Microservices:AuthService";

        /// <summary>
        /// Sets the app key to use for the profile service.
        /// </summary>
        public string ProfileServiceKey { private get; set; } = "Microservices:ProfileService";

        /// <summary>
        /// Sets the app key to use for the tweet service.
        /// </summary>
        public string TweetServiceKey { private get; set; } = "Microservices:TweetService";

        #endregion
    }
}
