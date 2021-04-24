using Kwetter.ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Service
{
    public class ProfileService
    {
        /// <summary>
        /// Access to the app configuration.
        /// </summary>
        private AppSettings settings;

        public ProfileService(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<Account> IncludeProfile(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
