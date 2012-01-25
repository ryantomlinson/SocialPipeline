using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class FindUserException : Exception
    {
        public FindUserException(string message) : base(message)
        {
            
        }
    }
}
