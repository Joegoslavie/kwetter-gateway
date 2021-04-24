using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Model
{
    public class Account
    {
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Profile Profile { get; set; }
    }
}
