namespace Kwetter.Business.Service
{
    using Grpc.Net.Client;
    using System;
    using System.Threading.Tasks;
    using Microservice.AuthGRPCService;
    using Kwetter.Business.Model;
    using Kwetter.Business.Factory;
    using System.Security.Authentication;

    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="configuration">Injected configuration.</param>
        public AuthenticationService(AppSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Tries to register a user in the authentication microservice.
        /// </summary>
        /// <param name="username">Username to register.</param>
        /// <param name="password">Associated password.</param>
        /// <returns></returns>
        public async Task<Account> Register(string username, string password)
        {
            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.RegisterAsync(new RegisterRequest { Username = username, Password = password, });
            });

            if (!response.Status)
            {
                throw new AuthenticationException(response.Message);
            }

            return AccountFactory.Parse(response);
        }

        /// <summary>
        /// Tries to sign in a user with username and password.
        /// </summary>
        /// <param name="username">Username string.</param>
        /// <param name="password">Password string.</param>
        /// <returns></returns>
        public async Task<Account> SignIn(string username, string password)
        {
            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.SignInAsync(new SignInRequest { Username = username, Password = password, });
            });

            if(!response.Status)
            {
                throw new AuthenticationException(response.Message);
            }

            return AccountFactory.Parse(response);
        }

        /// <summary>
        /// Create a service call.
        /// </summary>
        /// <typeparam name="TParsedResponse">Parsed response.</typeparam>
        /// <param name="responseHandler">Handler.</param>
        /// <returns></returns>
        private async Task<TParsedResponse> AuthenticationClientCall<TParsedResponse>(Func<AuthGRPCService.AuthGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.settings.AuthenticationServiceUrl))
            {
                var client = new AuthGRPCService.AuthGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
