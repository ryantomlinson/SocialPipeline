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
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        private readonly IDatabaseLogger logger;

        public CompanyRepository(IDatabaseLogger logger)
        {
            this.logger = logger;
        }

        /// <exception cref="UnknownCompanyException">Thrown when the company does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        public CompanyDto FindByName(string name)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var query = string.Format("select * from [Company] where [Name]='{0}'", name);
                    var user = connection.Query<CompanyDto>(query).Single<CompanyDto>();
                    return user;
                }
            }
            catch (InvalidOperationException exception)
            {
                logger.LogException("FindByName: Company does not exist", exception);
                throw new UnknownCompanyException("FindByName: Company does not exist");
            }
            catch (DbException exception)
            {
                logger.LogException("FindByName: Issue connecting to the database", exception);
                throw new SocialPipelineDatabaseConnectionException("FindByName: ");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByName: Unhandled exception", exception);
                throw;
            }
        }
    }
}
