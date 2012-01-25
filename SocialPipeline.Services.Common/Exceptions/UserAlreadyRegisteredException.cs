using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class UserAlreadyRegisteredException : Exception
    {
        public UserAlreadyRegisteredException(string message) : base(message)
        {
            
        }

        public UserAlreadyRegisteredException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
