using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels
{
    public class UpdateProfileViewModel
    {
        public string DisplayName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
