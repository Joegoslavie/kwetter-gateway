using Kwetter.ServiceLayer.Model;
using Kwetter.ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Manager
{
    class ProfileManager
    {
        private readonly ProfileService service;
        public ProfileManager(ProfileService service)
        {
            this.service = service;
        }

        public async Task<Profile> RetrieveProfile(Account account)
        {
            return null;
        }


    }
}
