using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.AuthGRPCService;

namespace Kwetter.ServiceLayer.Service
{
    public class AuthenticationService
    {
        /// <summary>
        /// The app key that contains the GRPC url.
        /// </summary>
        private readonly string appKey = "Kwetter.AuthService";

        /// <summary>
        /// The full GRPC endpoint address.
        /// </summary>
        private readonly string grpcEndpoint;

        public AuthenticationService(IConfiguration configuration)
        {
            this.grpcEndpoint = configuration.GetValue<string>(appKey);
        }

        /// <summary>
        /// Tries to register a user in the authentication microservice.
        /// </summary>
        /// <param name="username">Username to register.</param>
        /// <param name="password">Associated password.</param>
        /// <returns></returns>
        public async Task<bool> Register(string username, string password)
        {
            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.RegisterAsync(new RegisterRequest { Username = username, Password = password, });
            });

            return response.Status;
        }

        /// <summary>
        /// Tries to sign in a user with username and password.
        /// </summary>
        /// <param name="username">Username string.</param>
        /// <param name="password">Password string.</param>
        /// <returns></returns>
        public async Task<string> SignIn(string username, string password)
        {
            string token = string.Empty;

            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.SignInAsync(new SignInRequest { Username = username, Password = password, });
            });

            if (response.Status && !string.IsNullOrEmpty(response.Token))
            {
                token = response.Token;
            }

            return token;
        }

        private async Task<TParsedResponse> AuthenticationClientCall<TParsedResponse>(Func<AuthGRPCService.AuthGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.grpcEndpoint))
            {
                var client = new AuthGRPCService.AuthGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
