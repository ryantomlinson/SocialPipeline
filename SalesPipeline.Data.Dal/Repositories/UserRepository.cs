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
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;

namespace SocialPipeline.Data.Dal.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IDatabaseLogger logger;

        public UserRepository(IDatabaseLogger logger)
        {
            this.logger = logger;
        }

        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public SocialPipelineUserDto FindByEmailAddress(string email)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var query = string.Format("select * from [User] where Email='{0}'", email);
                    var user = connection.Query<SocialPipelineUserDto>(query).Single<SocialPipelineUserDto>();
                    return user;
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("FindByEmailAddress: User does not exist", exception);
                throw new UnknownUserException("FindByEmailAddress: User does not exist");
            }
            catch (DbException exception)
            {
                logger.LogException("FindByUserId: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("FindByEmailAddress: ");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByUserId: Unhandled exception", exception);
                throw;
            }
        }

        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public SocialPipelineUserDto FindByUserId(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var query = string.Format("select * from [User] where Id={0}", userId);
                    var user = connection.Query<SocialPipelineUserDto>(query).Single<SocialPipelineUserDto>();

                    if (user == null)
                        throw new UnknownUserException("FindByUserId: User does not exist");
                    return user;
                }
            }
            catch (UnknownUserException exception)
            {
                logger.LogException("FindByUserId: User does not exist", exception);
                throw new UnknownUserException("FindByUserId: User does not exist");
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("FindByUserId: User does not exist", exception);
                throw new UnknownUserException("FindByUserId: User does not exist");
            }
            catch (DbException exception)
            {
                logger.LogException("FindByUserId: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("FindByUserId: Database exception");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByUserId: Unhandled exception", exception);
                throw;
            }
        }

        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public List<SocialPipelineUserDto> FindAllUsersInCompanyByUserId(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var query = string.Format(@"select * from [User]
                                                where CompanyId = (
                                                select CompanyId from [User]
                                                where Id = {0})", userId);
                    var user = connection.Query<SocialPipelineUserDto>(query).ToList<SocialPipelineUserDto>();

                    return user;
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Users does not exist", exception);
                throw new UnknownUserException("FindAllUsersInCompanyByUserId: User does not exist");
            }
            catch (DbException exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("FindAllUsersInCompanyByUserId: Database exception");
            }
            catch (Exception exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Unhandled exception", exception);
                throw;
            }
        }
        ///<exception cref="UnableToAddUserException">Thrown when there is a sql issue inserting a new user</exception>
        public void AddUser(SocialPipelineUserDto userDto)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sqlInsertUserQuery = @"INSERT INTO [SalesPipeline].[dbo].[User]
                                               ([FirstName]
                                               ,[LastName]
                                               ,[Email]
                                               ,[Password]
                                               ,[IsActive]
                                               ,[IsAdmin]
                                               ,[CompanyId])
                                         VALUES
                                               (@FirstName
                                               ,@LastName
                                               ,@Email
                                               ,@Password
                                               ,@IsActive
                                               ,@IsAdmin
                                               ,@CompanyId)
                                        select cast(SCOPE_IDENTITY() as int)";

                        var createdUserId = connection.Query<int>(sqlInsertUserQuery, new
                                                                                 {
                                                                                     userDto.FirstName,
                                                                                     userDto.LastName,
                                                                                     userDto.Email,
                                                                                     userDto.Password,
                                                                                     IsActive = true,
                                                                                     userDto.IsAdmin,
                                                                                     userDto.CompanyId
                                                                                 }).Single();
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("AddUser: Problem adding a new user", exception);
                throw new UnableToAddUserException("AddUser: Problem adding a new user");
            }
            catch (DbException exception)
            {
                logger.LogException("AddUser: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("AddUser: Database exception");
            }
            catch (Exception exception)
            {
                logger.LogException("AddUser: Unhandled exception", exception);
                throw;
            }
        }
    }
}
