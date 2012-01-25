using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class FindOrganisationException : Exception
    {
        public FindOrganisationException(string message) : base(message)
        {
            
        }

        public FindOrganisationException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
