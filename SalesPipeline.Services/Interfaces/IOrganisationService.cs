using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Contracts;
using SocialPipeline.Services.Models.Entities.Organisation;

namespace SocialPipeline.Services.Interfaces
{
    [ContractClass(typeof(OrganisationServiceContract))]
    public interface IOrganisationService
    {
        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        Organisation GetOrganisation(string organisationName);

        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        Organisation GetOrganisation(int organisationId);

        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        List<Organisation> GetOrganisationsByCompanyId(int companyId); 

        /// <exception cref="SocialPipeline.Services.Common.Exceptions.UnableToAddOrganisationException">Thrown when there are issues adding a new organisation.</exception>
        void AddOrganisation(Organisation organisation);
    }
}
