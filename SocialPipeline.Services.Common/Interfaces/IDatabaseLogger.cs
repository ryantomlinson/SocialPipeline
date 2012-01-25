using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Interfaces
{
    public interface IDatabaseLogger
    {
        void LogException(string message, Exception exception = null);
    }
}
