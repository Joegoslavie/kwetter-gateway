using Kwetter.ServiceLayer.Model;
using Kwetter.ServiceLayer.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Manager
{
    public class AuthenticationManager
    {
        private readonly ILogger<AuthenticationManager> logger;

        private readonly AuthenticationService authService;

        private readonly ProfileService profileService;

        public AuthenticationManager(ILogger<AuthenticationManager> logger, AuthenticationService authenticationService, ProfileService profileService)
        {
            this.logger = logger;
            this.authService = authenticationService;
            this.profileService = profileService;
        }

        /// <summary>
        /// Tries to sign in a user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public async Task<Account> SignIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(username));
            }

            // todo: add calls to other services;

            var account = await this.authService.SignIn(username, password).ConfigureAwait(false);
            this.logger.LogInformation($"User {username} authentication successfully");

            return await this.CompleteProfile(account).ConfigureAwait(false);
        }

        /// <summary>
        /// Tries to sign up a user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns></returns>
        public async Task<Account> Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var account = await this.authService.Register(username, password).ConfigureAwait(false);
            this.logger.LogInformation($"@{username} created by user");

            return await this.CompleteProfile(account).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves addition properties of a newly signed in user. This will retrieve the <see cref="Account.Profile"/> and 
        /// <see cref="Account.Tweets"/> properties.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>Updated account properties.</returns>
        private async Task<Account> CompleteProfile(Account account)
        {
            await this.profileService.IncludeProfile(account).ConfigureAwait(false);
            return account;
        }
    }
}
