using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Authentication
{
    /// <summary>
    /// ViewModel class used for the registration process.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Gets or sets the repeated password for validation.
        /// </summary>
        public string PasswordRepeated { get; set; }
    }
}
