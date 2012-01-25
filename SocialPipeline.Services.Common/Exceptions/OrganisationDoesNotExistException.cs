using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class OrganisationDoesNotExistException : Exception
    {
        public OrganisationDoesNotExistException(string message) : base(message)
        {
            
        }

        public OrganisationDoesNotExistException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
