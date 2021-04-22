using Grpc.Net.Client;
using Kwetter.AuthenticationGRPCService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kwetter.AuthenticationGRPCService.AuthenticationGRPCService;

namespace Kwetter.ServiceLayer.Service
{
    public class AuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string appKey = "Microservice.Auth";

        /// <summary>
        /// 
        /// </summary>
        private readonly string grpcEndpoint;

        public AuthenticationService()
        {
            this.grpcEndpoint = ConfigurationManager.AppSettings[appKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Register(string username, string password)
        {
            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.SignInAsync(new SignInRequest { Username = username, Password = password, });
            });

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> SignIn(string username, string password)
        {
            var response = await this.AuthenticationClientCall(async client =>
            {
                return await client.RegisterAsync(new RegisterRequest { Username = username, Password = password, });
            });

            return "";
        }

        private async Task<TParsedResponse> AuthenticationClientCall<TParsedResponse>(Func<AuthenticationGRPCServiceClient, Task<TParsedResponse>> responseHandler)
        {
            using (var channel = GrpcChannel.ForAddress(this.grpcEndpoint))
            {
                var client = new AuthenticationGRPCServiceClient(channel);
                return await responseHandler(client);
            }
        }
    }
}
