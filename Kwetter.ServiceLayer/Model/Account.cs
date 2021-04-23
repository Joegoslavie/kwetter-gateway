using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Model
{
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        public string Password { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Profile Profile { get; set; }
    }
}
