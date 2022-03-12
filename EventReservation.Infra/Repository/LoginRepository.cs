using Dapper;

using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Infra.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbContext DbContext;
        public LoginRepository(IDbContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public List<Login> GetAllLogin()
        {
            IEnumerable<Login> result = DbContext.Connection.Query<Login>("LOGIN_PACKAGE.GETALLLOGIN", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }


        public bool AddLogin(Login login)
        {
            var p = new DynamicParameters();
            p.Add("UNAME", login.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSHASH", login.Passwordhash, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSALT", login.Passwordsalt, dbType: DbType.String, direction: ParameterDirection.Input);
            

            var result = DbContext.Connection.ExecuteAsync("LOGIN_PACKAGE.CREATELOGIN", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }

        public bool DeleteLogin(int id)
        {
            var p = new DynamicParameters();
            p.Add("LogID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.ExecuteAsync("LOGIN_PACKAGE.DELETELOGIN", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }



        public Login GetLoginById(int Id)
        {
            var p = new DynamicParameters();
            p.Add("id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.QueryFirstOrDefault<Login>("LOGIN_PACKAGE.GETLOGINBYID", p, commandType: CommandType.StoredProcedure);

            return result;
        }

        public bool UpdateLogin(LoginToUpdateDTO loginToUpdate)
        {
            var p = new DynamicParameters();
            p.Add("LogID", loginToUpdate.Loginid, dbType: DbType.Int32, direction: ParameterDirection.Input);

  
            p.Add("UNAME", loginToUpdate.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSHASH", loginToUpdate.Passwordhash, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSALT", loginToUpdate.Passwordsalt, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = DbContext.Connection.ExecuteAsync("LOGIN_PACKAGE.UPDATELOGIN", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }

        public Login Auth(Login login)
        {
            var p = new DynamicParameters();
            p.Add("UNAME", login.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSHASH", login.Passwordhash, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("PASSALT", login.Passwordsalt, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = DbContext.Connection.QueryFirstOrDefault<Login>("LOGIN_PACKAGE.USERLOGIN", p, commandType: CommandType.StoredProcedure);

            return result;


        }
    }
}
