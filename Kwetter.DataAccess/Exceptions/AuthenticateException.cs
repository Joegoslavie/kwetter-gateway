﻿namespace Kwetter.DataAccess.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Authentication exception class.
    /// </summary>
    public class AuthenticateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public AuthenticateException(string message)
            : base (message)
        {
        }
    }
}
