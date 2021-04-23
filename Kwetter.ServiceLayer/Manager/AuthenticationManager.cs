using Kwetter.ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Manager
{
    public class AuthenticationManager
    {
        private readonly AuthenticationService service = null;
        public AuthenticationManager(AuthenticationService service)
        {
            this.service = service;
        }

        public async Task<string> TrySignIn(string username, string password)
        {
           var token = await this.service.SignIn(username, password).ConfigureAwait(false);
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Failed to authenticate user.", nameof(username));
            }

            return token;
        }

        public async Task<bool> TrySignUp(string username, string password)
        {
            return await this.service.Register(username, password).ConfigureAwait(false);
        }
    }
}
