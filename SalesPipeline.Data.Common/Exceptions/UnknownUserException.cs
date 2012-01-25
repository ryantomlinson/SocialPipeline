using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Common.Exceptions
{
    public class UnknownUserException : Exception
    {
        public UnknownUserException(string message) : base(message)
        {
            
        }
    }
}
