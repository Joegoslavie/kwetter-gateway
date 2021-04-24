using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer
{
    public class AppSettings
    {
        /// <summary>
        /// Configuration class.
        /// </summary>
        private readonly IConfiguration config;

        public AppSettings(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationServiceUrl => this.config.GetValue<string>(this.AuthServiceKey);
        
        /// <summary>
        /// 
        /// </summary>
        public string ProfileServiceUrl => this.config.GetValue<string>(this.ProfileServiceKey);

        /// <summary>
        /// 
        /// </summary>
        public string AuthServiceKey { private get; set; } = "Kwetter.AuthService";

        /// <summary>
        /// 
        /// </summary>
        public string ProfileServiceKey { private get; set; } = "Kwetter.ProfileService";
    }
}
