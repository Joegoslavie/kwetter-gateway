namespace Kwetter.Business.Manager
{
    using Grpc.Core;
    using Kwetter.Business.Exceptions;
    using Kwetter.DataAccess.Exceptions.Enum;
    using Kwetter.DataAccess.Model;
    using Kwetter.DataAccess.Service;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class AuthenticationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<AuthenticationManager> logger;

        /// <summary>
        /// 
        /// </summary>
        private readonly AuthenticationService authService;

        /// <summary>
        /// 
        /// </summary>
        private readonly ProfileService profileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationManager"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authenticationService"></param>
        /// <param name="profileService"></param>
        public AuthenticationManager(ILogger<AuthenticationManager> logger, AuthenticationService authenticationService, ProfileService profileService)
        {
            this.logger = logger;
            this.authService = authenticationService;
            this.profileService = profileService;
        }

        /// <summary>
        /// Tries to sign in a user, when succesfully authenticated, the <see cref="Account"/> of the user will be returned.
        /// </summary>
        /// <param name="username">Username string.</param>
        /// <param name="password">Password string.</param>
        /// <returns><see cref="Account"/> of the <paramref name="username"/>.</returns>
        public async Task<Account> SignIn(string username, string password)
        {

            #region null checks
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(username));
            }
            #endregion

            try
            {
                this.logger.LogInformation($"Authenticating {username}");
                var account = await this.authService.SignIn(username, password).ConfigureAwait(false);

                return account;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound || ex.StatusCode == StatusCode.Unauthenticated)
            {
                throw new AuthenticateException(KwetterError.Credentials, "Username and/or password incorrect", ex);
            }
            catch (RpcException ex)
            {
                this.logger.LogError("Exception occurred in authentication microservice", ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Tries to register an account in the database with the passed parameters.
        /// </summary>
        /// <param name="username">Username string.</param>
        /// <param name="password">Password string.</param>
        /// <param name="email">Email string.</param>
        /// <returns>Newly created <see cref="Account"/>.</returns>
        public async Task<Account> Register(string username, string password, string email)
        {
            #region null checks
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            #endregion

            try
            {
                var account = await this.authService.Register(username, password, email).ConfigureAwait(false);
                this.logger.LogInformation($"@{username} created by user");

                return account;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.AlreadyExists || ex.StatusCode == StatusCode.Unauthenticated)
            {
                throw new AuthenticateException(KwetterError.UsernameTaken, "Username is already taken", ex);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
            {
                throw new AuthenticateException(KwetterError.EmailTaken, ex.Message, ex);
            }
            catch (RpcException ex)
            {
                this.logger.LogError("Exception occurred in authentication microservice", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
