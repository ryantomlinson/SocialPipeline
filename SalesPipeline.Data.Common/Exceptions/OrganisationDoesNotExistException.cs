using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Common.Exceptions
{
    public class OrganisationDoesNotExistException : Exception
    {
        public OrganisationDoesNotExistException(string message) : base(message)
        {
            
        }
    }
}
