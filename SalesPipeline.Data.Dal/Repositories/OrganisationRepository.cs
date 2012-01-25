using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Services.Common.Interfaces;
using OrganisationDoesNotExistException = SocialPipeline.Data.Common.Exceptions.OrganisationDoesNotExistException;

namespace SocialPipeline.Data.Dal.Repositories
{
    public class OrganisationRepository : BaseRepository, IOrganisationRepository
    {
        private readonly IDatabaseLogger logger;

        public OrganisationRepository(IDatabaseLogger logger)
        {
            this.logger = logger;
        }

        /// <exception cref="OrganisationDoesNotExistException">Thrown when the organisation does not exist</exception>
        public OrganisationDto GetOrganisation(string organisationName)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var query = string.Format(@"select *
                                            from Organisation o 
                                            inner join [User] u on u.id = o.userid
                                            where o.Name='{0}'", organisationName);

                    var organisation = connection.Query<OrganisationDto, SocialPipelineUserDto, OrganisationDto>(query,
                        (org, socialPipelineUserDto) => { org.CreatedBy = socialPipelineUserDto; return org; })
                        .Single<OrganisationDto>();

                    if (organisation == null)
                        throw new OrganisationDoesNotExistException("GetOrganisation: No organisation exists.");
                    return organisation;
                }
            }
            catch (OrganisationDoesNotExistException exception)
            {
                logger.LogException("GetOrganisation: Unable to find organisation", exception);
                throw;
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisation: Unable to find organisation", exception);
                throw;
            }
        }

        /// <exception cref="OrganisationDoesNotExistException">Thrown when the organisation does not exist</exception>
        public OrganisationDto GetOrganisation(int organisationId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var query = string.Format(@"select *
                                            from Organisation o 
                                            inner join [User] u on u.id = o.userid
                                            where o.Id={0}", organisationId);

                    var organisation = connection.Query<OrganisationDto, SocialPipelineUserDto, OrganisationDto>(query,
                        (org, socialPipelineUserDto) => { org.CreatedBy = socialPipelineUserDto; return org; })
                        .Single<OrganisationDto>();

                    if (organisation == null)
                        throw new OrganisationDoesNotExistException("GetOrganisation: No organisation exists.");
                    return organisation;
                }
            }
            catch (OrganisationDoesNotExistException exception)
            {
                logger.LogException("GetOrganisation: Unable to find organisation", exception);
                throw;
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisation: Unable to find organisation", exception);
                throw;
            }
        }

        /// <exception cref="OrganisationDoesNotExistException">Thrown when there are no organisations for a company</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public List<OrganisationDto> GetOrganisationsByCompanyId(int companyId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //var query = string.Format("select * from Organisation where CompanyId={0}", companyId);
                    var query = string.Format(@"select o.[Id]
                                                  ,o.[Name]
                                                  ,o.[DateAdded]
                                                  ,o.[Active]
                                                  ,o.[CompanyId]
                                                  ,u.[Id] 
                                                  ,u.[FirstName]
                                                  ,u.[LastName]
                                                  ,u.[Email]
                                                  ,u.[Password]
                                                  ,u.[RegisteredDate]
                                                  ,u.[LastLoggedInDate]
                                                  ,u.[IsActive]
                                                  ,u.[IsAdmin]
                                                  ,u.[CompanyId]
                                            from Organisation o 
                                            inner join [User] u on u.id = o.userid
                                            where o.CompanyId={0}", companyId);

                    var organisationList = connection.Query<OrganisationDto, SocialPipelineUserDto, OrganisationDto>(query,
                        (organisation, socialPipelineUserDto) => { organisation.CreatedBy = socialPipelineUserDto; return organisation; }).ToList<OrganisationDto>();

                    return organisationList;
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("GetOrganisationsByCompanyId: Organisation does not exist", exception);
                throw new OrganisationDoesNotExistException("GetOrganisationsByCompanyId: Organisation does not exist");
            }
            catch (DbException exception)
            {
                logger.LogException("GetOrganisationsByCompanyId: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("GetOrganisationsByCompanyId: Database exception");
            }
            catch (Exception exception)
            {
                logger.LogException("GetOrganisationsByCompanyId: Unhandled exception", exception);
                throw;
            }
        }

        /// <exception cref="UnableToAddOrganisationException">Thrown when unable to add organisation</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public void AddOrganisation(OrganisationDto organisationDto)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    
                    string sqlInsertOrganisationQuery = @"INSERT INTO [SalesPipeline].[dbo].[Organisation]
                                                       ([Name]
                                                       ,[CompanyId]
                                                       ,[UserId])
                                                 VALUES
                                                       (@Name
                                                       ,@CompanyId
                                                       ,@UserId)";

                        connection.Execute(sqlInsertOrganisationQuery, new
                                                                           {
                                                                               Name = organisationDto.Name,
                                                                               CompanyId = organisationDto.CreatedBy.CompanyId,
                                                                               UserId = organisationDto.CreatedBy.Id
                                                                           });
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("AddOrganisation: Unable to add organisation", exception);
                throw new UnableToAddOrganisationException("AddOrganisation: Unable to add organisation");
            }
            catch (DbException exception)
            {
                logger.LogException("AddOrganisation: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("AddOrganisation: Database exception");
            }
            catch (Exception exception)
            {
                logger.LogException("AddOrganisation: Unhandled exception", exception);
                throw;
            }
        }
    }
}
