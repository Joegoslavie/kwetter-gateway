using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels
{
    public class UpdateProfileViewModel
    {
        public string DisplayName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Location { get; set; }

        [StringLength(160, MinimumLength = 3, ErrorMessage = "Max 160 characters")]
        public string Description { get; set; }
    }
}
