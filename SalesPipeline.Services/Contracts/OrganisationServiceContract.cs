using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Organisation;

namespace SocialPipeline.Services.Contracts
{
    [ContractClassFor(typeof(IOrganisationService))]
    public class OrganisationServiceContract : IOrganisationService
    {
        public Organisation GetOrganisation(string organisationName)
        {
            Contract.Requires(!string.IsNullOrEmpty(organisationName));

            return default(Organisation);
        }

        public Organisation GetOrganisation(int organisationId)
        {
            Contract.Requires(organisationId > 0);

            return default(Organisation);
        }

        public List<Organisation> GetOrganisationsByCompanyId(int companyId)
        {
            Contract.Requires(companyId > 0);

            return default(List<Organisation>);
        }

        public void AddOrganisation(Organisation organisation)
        {
            Contract.Requires(!string.IsNullOrEmpty(organisation.Name));
            Contract.Requires(organisation.CreatedBy.CompanyId > 0);
            Contract.Requires(organisation.CreatedBy.Id > 0);
        }
    }
}
