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

        public AuthenticationResultModel(int errorCode, string message)
        {
            this.ErrorCode = errorCode;
            this.Succeeded = false;
            this.Message = message;
        }

        public AuthenticationResultModel()
        {

        }

        public bool Succeeded { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public AccountViewModel Account { get; set; }
    }
}
