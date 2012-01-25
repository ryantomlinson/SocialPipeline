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

namespace SocialPipeline.Data.Dal.Repositories
{
    public class RegistrationRepository : BaseRepository, IRegistrationRepository
    {
        private readonly IDatabaseLogger logger;

        public RegistrationRepository(IDatabaseLogger logger)
        {
            this.logger = logger;
        }

        /// <exception cref="RegistrationException">Thrown when there are issues registering the user</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are database issues</exception>
        public int RegisterUser(SocialPipelineUserDto userDto, CompanyDto companyDto)
        {
            SqlTransaction transaction = null;
            int userId = -1;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction("registeruser");

                    string sqlInsertCompanyQuery = @"INSERT INTO [SalesPipeline].[dbo].[Company] ([Name]) VALUES (@Name);select cast(SCOPE_IDENTITY() as int)";

                    var companyId = connection.Query<int>(sqlInsertCompanyQuery, 
                        new{
                            Name = companyDto.Name
                        }, transaction).Single();

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
                                                                                 IsAdmin = true,
                                                                                 CompanyId = companyId
                                                                             }, transaction).Single();
                    userId = createdUserId;

                    
                    transaction.Commit();
                }

                if (userId < 1)
                    throw new RegistrationException("RegisterUser: Unable to register user");
                return userId;
            }
            catch (InvalidOperationException exception)
            {
                if (transaction != null) transaction.Rollback("registeruser");
                logger.LogException("RegisterUser: User does not exist", exception);
                throw new RegistrationException("RegisterUser: User does not exist");
            }
            catch (DbException exception)
            {
                if (transaction != null) transaction.Rollback("registeruser");
                logger.LogException("RegisterUser: User does not exist", exception);
                throw new SocialPipelineDatabaseConnectionException("FindByUserId: ");
            }
            catch(RegistrationException exception)
            {
                if (transaction != null) transaction.Rollback("registeruser");
                logger.LogException("RegisterUser: User does not exist", exception);
                throw;
            }
            catch (Exception exception)
            {
                if (transaction != null) transaction.Rollback("registeruser");
                logger.LogException("RegisterUser: Unhandled exception", exception);
                throw;
            }
        }
    }
}
