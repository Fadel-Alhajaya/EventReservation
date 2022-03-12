using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Repository
{
    public class AboutusRepository: IAboutusRepository
    {
        private readonly IDbContext _dbContext;

        public AboutusRepository(IDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public async Task<Aboutus> AddAboutus(Aboutus aboutus)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("DES", aboutus.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("IMG", aboutus.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WEBID", aboutus.Websiteid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("ABOUTUS_F_PACKAGE.CREATEABOUTUS", parmeter, commandType: CommandType.StoredProcedure);

            return aboutus;

        }

        public async Task<bool> DeleteAboutus(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("ABOUTUS_F_PACKAGE.DELETEABOUTUS", parmeter, commandType: CommandType.StoredProcedure);

            return true;

        }

        public async Task<Aboutus> GetAboutusById(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result=  await _dbContext.Connection.QueryFirstOrDefaultAsync<Aboutus>("ABOUTUS_F_PACKAGE.GETABOUTUSBYID", parmeter, commandType: CommandType.StoredProcedure);

            return result;

        }

        public async Task<Aboutus> GetAllAboutus()
        {
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Aboutus>("ABOUTUS_F_PACKAGE.GETALLABOUTUS", commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> UpdateAboutus(Aboutus aboutus)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", aboutus.Aboutusid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("DES", aboutus.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("IMG", aboutus.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WEBID", aboutus.Websiteid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("ABOUTUS_F_PACKAGE.UPDATEABOUTUS", parmeter, commandType: CommandType.StoredProcedure);

            return true;

        }
    }
}
