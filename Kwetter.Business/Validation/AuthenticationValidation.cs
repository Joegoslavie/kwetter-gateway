using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Validation
{
    public class AuthenticationValidation
    {
        /// <summary>
        /// Validates that the passed <paramref name="password"/> is not null or empty.
        /// </summary>
        /// <param name="password">Password string.</param>
        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
        }

        /// <summary>
        /// Validates that the passed passwords are not null or empty and are identical.
        /// </summary>
        /// <param name="password">Password string.</param>
        /// <param name="repeat">Repeated password string.</param>
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
        /// Validates that the passed <paramref name="username"/> is not null or empty.
        /// </summary>
        /// <param name="username">Username string.</param>
        public static void ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) 
            {
                throw new ArgumentNullException(nameof(username));
            }
        }
    }
}
