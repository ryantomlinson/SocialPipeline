using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class FindCompanyException : Exception
    {
        public FindCompanyException(string message) : base(message)
        {
            
        }
        public FindCompanyException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
