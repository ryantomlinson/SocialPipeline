using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;

namespace SocialPipeline.Data.Dal.Interfaces
{
    public interface IOrganisationRepository
    {
        /// <exception cref="OrganisationDoesNotExistException">Thrown when the organisation does not exist</exception>
        OrganisationDto GetOrganisation(string organisationName);

        /// <exception cref="OrganisationDoesNotExistException">Thrown when the organisation does not exist</exception>
        OrganisationDto GetOrganisation(int organisationId);

        /// <exception cref="OrganisationDoesNotExistException">Thrown when there are no organisations for a company</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        List<OrganisationDto> GetOrganisationsByCompanyId(int companyId); 

        /// <exception cref="UnableToAddOrganisationException">Thrown when unable to add organisation</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        void AddOrganisation(OrganisationDto organisationDto);
    }
}
