using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class UnableToRegisterUserException : Exception
    {
        public UnableToRegisterUserException(string message) : base(message)
        {
            
        }

        public UnableToRegisterUserException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
