using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Business.Exceptions
{
    public class ProfileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public ProfileException(string message)
            : base(message)
        {
        }
    }
}
