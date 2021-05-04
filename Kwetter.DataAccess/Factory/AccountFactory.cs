namespace Kwetter.DataAccess.Factory
{
    using Kwetter.DataAccess.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microservice.AuthGRPCService;

    class AccountFactory
    {
        public static Account Parse(AuthenticationResponse response)
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
