using Kwetter.ServiceLayer.Model;
using Microservice.AuthGRPCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Factory
{
    public class AccountFactory
    {
        public static Account FromResponse(AuthenticationResponse response)
        {
            if (response == null || response.Account == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return new Account
            {
                Id = response.Account.Id,
                Username = response.Account.Username,
                Email = response.Account.Email,
            };
        }
    }
}
