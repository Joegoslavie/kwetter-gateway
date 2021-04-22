using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Models
{
    public class Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebsiteUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Tweet> Tweets { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Account> Following { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Account> Followers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Account> Blocked { get; set; }
    }
}
