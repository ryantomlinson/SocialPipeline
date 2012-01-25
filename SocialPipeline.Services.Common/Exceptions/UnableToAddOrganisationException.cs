using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class UnableToAddOrganisationException : Exception
    {
        public UnableToAddOrganisationException(string message) : base(message)
        {
            
        }

        public UnableToAddOrganisationException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
