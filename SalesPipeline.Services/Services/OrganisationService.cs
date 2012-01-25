using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Services.Models.Entities.Organisation;
using OrganisationDoesNotExistException = SocialPipeline.Data.Common.Exceptions.OrganisationDoesNotExistException;
using UnableToAddOrganisationException = SocialPipeline.Data.Common.Exceptions.UnableToAddOrganisationException;

namespace SocialPipeline.Services.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationRepository organisationRepository;
        private readonly IDatabaseLogger logger;

        public OrganisationService(IOrganisationRepository organisationRepository, IDatabaseLogger logger)
        {
            this.organisationRepository = organisationRepository;
            this.logger = logger;
        }

        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        public Organisation GetOrganisation(string organisationName)
        {
            try
            {
                var organisationDto = organisationRepository.GetOrganisation(organisationName);
                var organisation = Mapper.Map<OrganisationDto, Organisation>(organisationDto);

                return organisation;
            }
            catch (OrganisationDoesNotExistException exception)
            {
                logger.LogException(string.Format("GetOrganisation: There is no organisation {0}", organisationName), exception);
                throw new FindOrganisationException(string.Format("GetOrganisation: There is no organisation {0}", organisationName), exception);
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisation: Unhandled exception", exception);
                throw new FindOrganisationException(string.Format("GetOrganisation: There is no organisation {0}", organisationName), exception);
            }
        }

        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        public Organisation GetOrganisation(int organisationId)
        {
            try
            {
                var organisationDto = organisationRepository.GetOrganisation(organisationId);
                var organisation = Mapper.Map<OrganisationDto, Organisation>(organisationDto);

                return organisation;
            }
            catch (OrganisationDoesNotExistException exception)
            {
                logger.LogException(string.Format("GetOrganisation: There is no organisation with Id {0}", organisationId), exception);
                throw new FindOrganisationException(string.Format("GetOrganisation: There is no organisation with Id {0}", organisationId), exception);
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisation: Unhandled exception", exception);
                throw new FindOrganisationException(string.Format("GetOrganisation: There is no organisation with Id {0}", organisationId), exception);
            }
        }

        /// <exception cref="FindOrganisationException">Thrown when there are issues finding a specified organisation.</exception>
        public List<Organisation> GetOrganisationsByCompanyId(int companyId)
        {
            try
            {
                var organisationDtos = organisationRepository.GetOrganisationsByCompanyId(companyId);

                var organisations = new List<Organisation>(organisationDtos.Count);
                //organisations.AddRange(organisationDtos.Select(Mapper.Map<OrganisationDto, Organisation>()));
                foreach (var organisationDto in organisationDtos)
                {
                    organisations.Add(Mapper.Map<OrganisationDto, Organisation>(organisationDto));
                }
 
                return organisations;
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("GetOrganisationsByCompanyId: Unable to find users due to database connection issue.", exception);
                throw new FindOrganisationException("GetOrganisationsByCompanyId: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisationsByCompanyId: Unhandled Exception", exception);
                throw new FindOrganisationException("GetOrganisationsByCompanyId: Unknown issue.");
            }
        }

        /// <exception cref="SocialPipeline.Services.Common.Exceptions.UnableToAddOrganisationException">Thrown when there are issues adding a new organisation.</exception>
        public void AddOrganisation(Organisation organisation)
        {
            try
            {
                var organisationDto = Mapper.Map<Organisation, OrganisationDto>(organisation);
                organisationRepository.AddOrganisation(organisationDto);
            }
            catch (UnableToAddOrganisationException exception)
            {
                logger.LogException("AddOrganisation: Unable to add organisation due to database connection issue.", exception);
                throw new SocialPipeline.Services.Common.Exceptions.UnableToAddOrganisationException("AddOrganisation: Problems connecting to the database.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("AddOrganisation: Unable to add organisation due to database connection issue.", exception);
                throw new SocialPipeline.Services.Common.Exceptions.UnableToAddOrganisationException("AddOrganisation: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("AddOrganisation: Unhandled Exception", exception);
                throw new SocialPipeline.Services.Common.Exceptions.UnableToAddOrganisationException("AddOrganisation: Unknown issue.");
            }
        }
    }
}
