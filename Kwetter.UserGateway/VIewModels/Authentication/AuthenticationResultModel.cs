using Kwetter.UserGateway.VIewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Authentication
{
    public class AuthenticationResultModel
    {
        public AuthenticationResultModel(AccountViewModel account)
        {
            this.Succeeded = true;
            this.Account = account;
        }

        public AuthenticationResultModel(bool succeeded, string message, AccountViewModel account)
        {
            this.Succeeded = succeeded;
            this.Message = message;
            this.Account = account;
        }

        public bool Succeeded { get; }
        public string Message { get; }
        public AccountViewModel Account { get; }
    }
}
