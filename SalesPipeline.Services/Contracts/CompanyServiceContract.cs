using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Company;

namespace SocialPipeline.Services.Contracts
{
    [ContractClassFor(typeof(ICompanyService))]
    public class CompanyServiceContract : ICompanyService
    {
        public Company FindByName(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));

            return default(Company);
        }
    }
}
