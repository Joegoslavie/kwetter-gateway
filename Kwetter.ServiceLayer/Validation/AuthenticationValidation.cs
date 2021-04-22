using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.ServiceLayer.Validation
{
    public class AuthenticationValidation
    {
        // Todo: extend and enforce password policy too.

        public static void ValidatePassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
        }

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

        public static void ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) 
            {
                throw new ArgumentNullException(nameof(username));
            }
        }
    }
}
