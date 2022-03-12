using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO.WebsiteDto;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Repository
{
    public class WebsiteRepository:IWebsiteRepository
    {

        private readonly IDbContext _dbContext;

        public WebsiteRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<bool> AddWebsite(AddToWebsiteDto addToWebsiteDto)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("WNAME", addToWebsiteDto.Websitename, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LPATH", addToWebsiteDto.Logopath, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("BGROUND", addToWebsiteDto.Backgroundimg, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("TPHONE", addToWebsiteDto.Telephone, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("EMAILADDRESS", addToWebsiteDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WADDRESS", addToWebsiteDto.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WTIME", addToWebsiteDto.Worktime, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("balance", addToWebsiteDto.Wallet, dbType: DbType.Double, direction: ParameterDirection.Input);
            parmeter.Add("AID", addToWebsiteDto.Adminid, dbType: DbType.Double, direction: ParameterDirection.Input);


            await _dbContext.Connection.QueryAsync("WEBSITE_F_PACKAGE.CREATEWEBSITE", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> DeleteWebsite(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            await _dbContext.Connection.QueryAsync("WEBSITE_F_PACKAGE.DELETEWEBSITE", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }
        public async Task<bool> UpdateWebsite(UpdateToWebsiteDto updateToWebsiteDto)
        {


            var parmeter = new DynamicParameters();
            parmeter.Add("ID", updateToWebsiteDto.Websiteid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("WNAME", updateToWebsiteDto.Websitename, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LPATH", updateToWebsiteDto.Logopath, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("BGROUND", updateToWebsiteDto.Backgroundimg, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("TPHONE", updateToWebsiteDto.Telephone, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("EMAILADDRESS", updateToWebsiteDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WADDRESS", updateToWebsiteDto.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WTIME", updateToWebsiteDto.Worktime, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("balance", updateToWebsiteDto.Wallet, dbType: DbType.Double, direction: ParameterDirection.Input);
            parmeter.Add("AID", updateToWebsiteDto.Adminid, dbType: DbType.Int32, direction: ParameterDirection.Input);


            await _dbContext.Connection.QueryAsync("WEBSITE_F_PACKAGE.UPDATEWEBSITE", parmeter, commandType: CommandType.StoredProcedure);

            return true;



        }

       
        public async Task<List<Website>> GetAllWebsite()
        {
            var result = await _dbContext.Connection.QueryAsync<Website>("WEBSITE_F_PACKAGE.GETALLWEBSITE", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<Website> GetWebsiteById(int id)
        {

            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

           var result= await _dbContext.Connection.QueryFirstOrDefaultAsync<Website>("WEBSITE_F_PACKAGE.GETWEBSITEBYID", parmeter, commandType: CommandType.StoredProcedure);

            return result ;


        }

        public async Task<List<Website>> SearchWebsiteByName(string name)
        {

            var parmeter = new DynamicParameters();
            parmeter.Add("WNAME", name, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Website>("WEBSITE_F_PACKAGE.SEARCHBYNAME",parmeter ,commandType: CommandType.StoredProcedure);



            return result.ToList();



        }


        public async Task<bool> EmailExsists(string email)
        {

            var parmeter = new DynamicParameters();
            parmeter.Add("EMAILADDRESS", email, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Website>("WEBSITE_F_PACKAGE.EMAILEXSISTS", parmeter, commandType: CommandType.StoredProcedure);

            if (result !=null )

                return true;

            return false;
        }


    }
}
