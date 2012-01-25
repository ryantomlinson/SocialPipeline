using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Exceptions
{
    public class CompanyAlreadyExistsException : Exception
    {
        public CompanyAlreadyExistsException(string message) : base(message)
        {
            
        }

        public CompanyAlreadyExistsException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
