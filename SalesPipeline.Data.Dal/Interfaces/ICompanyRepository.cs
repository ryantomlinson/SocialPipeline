using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;

namespace SocialPipeline.Data.Dal.Interfaces
{
    public interface ICompanyRepository
    {
        /// <exception cref="UnknownCompanyException">Thrown when the company does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        CompanyDto FindByName(string name);
    }
}
