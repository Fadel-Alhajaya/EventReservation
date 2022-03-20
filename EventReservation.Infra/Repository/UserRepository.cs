using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<List<UsertoResultDto>> GetAllUsers()
        {

           var users = await _dbContext.Connection.QueryAsync<UsertoResultDto>("USER_F_PACKAGE.GETALLUSERS", commandType: CommandType.StoredProcedure);

            return users.ToList();
           

        }


        public async Task<bool> AddAdmin(AddToAdminDto addToAdminDto)
        {

            var parmeterLogin = new DynamicParameters();
            var AdminLog = new Login();
            CreatePasswordHash(addToAdminDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            AdminLog.Passwordhash = passwordHash;
            AdminLog.Passwordsalt = passwordSalt;
            parmeterLogin.Add("UNAME", addToAdminDto.Username , dbType: DbType.String, direction: ParameterDirection.Input);
            parmeterLogin.Add("PASSALT", AdminLog.Passwordsalt, dbType: DbType.Binary, direction: ParameterDirection.Input);
            parmeterLogin.Add("PASSHASH", AdminLog.Passwordhash, dbType: DbType.Binary, direction: ParameterDirection.Input);

            var result=  await _dbContext.Connection.QueryFirstAsync<Login>("LOGIN_PACKAGE.CREATELOGIN", parmeterLogin, commandType: CommandType.StoredProcedure);
           
          
            

            var parmeter = new DynamicParameters();
            parmeter.Add("FNAME", addToAdminDto.Firstname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LNAME", addToAdminDto.Lastname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("EMAILADDRESS", addToAdminDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("BDATE", addToAdminDto.Birthdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parmeter.Add("IMG", addToAdminDto.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LID", result.id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("CID", null, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("RID", 2, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("PID", addToAdminDto.PublicId, dbType: DbType.String, direction: ParameterDirection.Input);




            await _dbContext.Connection.QueryAsync("USER_F_PACKAGE.CREATEUSER", parmeter, commandType: CommandType.StoredProcedure);


            return true;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };


        }

        public async Task<bool> DeleteUser(int id)
        {
            var parameter = new DynamicParameters();

            parameter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryFirstAsync<User>("USER_F_PACKAGE.DELETEUSER", parameter, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            var parameterLogin = new DynamicParameters();
            parameterLogin.Add("LogID", result.Loginid, dbType: DbType.Int32, direction: ParameterDirection.Input);

           await _dbContext.Connection.QueryFirstAsync("LOGIN_PACKAGE.DELETELOGIN", parameterLogin, commandType: CommandType.StoredProcedure);


            return true;

        }



        public async Task<UsertoResultDto> GetUserById(int id)
        {

            var parameter = new DynamicParameters();

            parameter.Add("usernno", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<UsertoResultDto>("USER_F_PACKAGE.GETUSERBYID", parameter, commandType: CommandType.StoredProcedure);

            return result;

        }

        public async Task<List<UsertoResultDto>> SearchUser(UserToSearchDto userSearchDto)
        {
            var parameter = new DynamicParameters();
            parameter.Add("EMAILADDRESS", userSearchDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("FNAME", userSearchDto.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("LNAME", userSearchDto.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("ROLENAME", userSearchDto.RoleName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("UNAME", userSearchDto.UserName, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<UsertoResultDto>("USER_F_PACKAGE.SEARCHUSERS", parameter, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<bool> EmailExsists(string email)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("EMAILADDRESS", email, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>("USER_F_PACKAGE.EMAILEXSISTS", parmeter, commandType: CommandType.StoredProcedure);

            if (result != null)

                return true;

            return false;


        }

        public async Task<bool> UserNameExsists(string username)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("UNAME", username, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>("LOGIN_PACKAGE.USERNAMEESISTS", parmeter, commandType: CommandType.StoredProcedure);

            if (result != null)

                return true;

            return false;
        }

        public async Task<bool> EditProfile(UserToUpdateDto userToUpdateDto)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", userToUpdateDto.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("EMAILADDRESS", userToUpdateDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("BDATE", userToUpdateDto.Birthdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parmeter.Add("FNAME", userToUpdateDto.Firstname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LNAME", userToUpdateDto.Lastname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("IMG", userToUpdateDto.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("PID", userToUpdateDto.PublicId, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<UserToUpdateDto>("USER_F_PACKAGE.UPDATEUSER", parmeter, commandType: CommandType.StoredProcedure);

            if (result != null)
                return true;


            return false;
                    

        }
    }
}
