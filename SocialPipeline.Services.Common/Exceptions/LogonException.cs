using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class LogonException : Exception
    {
        public LogonException(string message) : base(message)
        {
            
        }
    }
}
