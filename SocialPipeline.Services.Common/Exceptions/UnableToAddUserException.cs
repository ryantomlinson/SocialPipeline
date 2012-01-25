using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class UnableToAddUserException : Exception
    {
        public UnableToAddUserException(string message) : base(message)
        {
            
        }

        public UnableToAddUserException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
