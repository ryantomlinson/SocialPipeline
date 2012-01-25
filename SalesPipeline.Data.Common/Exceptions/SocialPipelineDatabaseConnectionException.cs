using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Common.Exceptions
{
    public class SocialPipelineDatabaseConnectionException : Exception
    {
        public SocialPipelineDatabaseConnectionException(string message) : base(message)
        {
            
        }
    }
}
