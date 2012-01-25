using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Contracts;
using SocialPipeline.Services.Models.Entities.Company;

namespace SocialPipeline.Services.Interfaces
{
    [ContractClass(typeof(CompanyServiceContract))]
    public interface ICompanyService
    {
        /// <exception cref="FindCompanyException">Thrown when there are issues finding the company or the company does not exist.</exception>
        Company FindByName(string name);
    }
}
