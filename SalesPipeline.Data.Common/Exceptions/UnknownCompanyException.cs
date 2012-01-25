using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Common.Exceptions
{
    public class UnknownCompanyException : Exception
    {
        public UnknownCompanyException(string message) : base(message)
        {
            
        }
    }
}
