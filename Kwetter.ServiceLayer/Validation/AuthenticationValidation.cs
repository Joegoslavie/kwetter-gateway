using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Validation
{
    public class AuthenticationValidation
    {
        // Todo: extend and enforce password policy too.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        public static void ValidatePassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="repeat"></param>
        public static void ValidatePassword(string password, string repeat)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrEmpty(repeat))
            {
                throw new ArgumentNullException(nameof(repeat));
            }

            if (password != repeat)
            {
                throw new InvalidOperationException("Given passwords do not match");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public static void ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) 
            {
                throw new ArgumentNullException(nameof(username));
            }
        }
    }
}
