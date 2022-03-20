using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.DTO.UserDto;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Repository
{
    
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbContext _dbContext;

        public AuthRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LoginToDto> Login(UserToLoginDto userToLoginDto)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("UNAME", userToLoginDto.UserName, dbType: DbType.String, direction: ParameterDirection.Input);


            var result = await _dbContext.Connection.QueryFirstAsync<LoginToDto>("LOGIN_PACKAGE.USERLOGIN", parmeter, commandType: CommandType.StoredProcedure);
           
            if (result == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(userToLoginDto.Password, result.Passwordhash, result.Passwordsalt))
                return null;

            return result;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            };


        }
        
        public async Task<UserToResultRegisterDto> Register(UserToRegisterDto userToRegisterDto)
        {

            var parmeterLogin = new DynamicParameters();
            var userlogin = new Login();
            CreatePasswordHash(userToRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userlogin.Passwordhash = passwordHash;
            userlogin.Passwordsalt = passwordSalt;
            parmeterLogin.Add("UNAME", userToRegisterDto.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeterLogin.Add("PASSALT", userlogin.Passwordsalt, dbType: DbType.Binary, direction: ParameterDirection.Input);
            parmeterLogin.Add("PASSHASH", userlogin.Passwordhash, dbType: DbType.Binary, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryFirstAsync<Login>("LOGIN_PACKAGE.CREATELOGIN", parmeterLogin, commandType: CommandType.StoredProcedure);




            var parmeter = new DynamicParameters();
            parmeter.Add("FNAME", userToRegisterDto.Firstname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LNAME", userToRegisterDto.Lastname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("EMAILADDRESS", userToRegisterDto.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("BDATE", userToRegisterDto.Birthdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parmeter.Add("IMG", userToRegisterDto.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("LID", result.id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("CID", null, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("RID", 3, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("PID", userToRegisterDto.PublicId, dbType: DbType.String, direction: ParameterDirection.Input);


            var resultUser=  await _dbContext.Connection.QueryFirstAsync<UserToResultRegisterDto>("USER_F_PACKAGE.CREATEUSER", parmeter, commandType: CommandType.StoredProcedure);

           
            return resultUser;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };


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

        public async Task<bool> ChangePassword(PasswordToDto passwordToDto)
        {
            var parmeter = new DynamicParameters();

            parmeter.Add("id",passwordToDto.Logid, dbType: DbType.Int32, direction: ParameterDirection.Input);


            var result = await _dbContext.Connection.QueryFirstAsync<LoginToDto>("LOGIN_PACKAGE.GETLOGINBYID", parmeter, commandType: CommandType.StoredProcedure);
            if (!VerifyPasswordHash(passwordToDto.CurrentPassword, result.Passwordhash, result.Passwordsalt))


                return false;

            var parmeterPassword = new DynamicParameters();
            CreatePasswordHash(passwordToDto.newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            parmeterPassword.Add("LogID", passwordToDto.Logid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeterPassword.Add("PASSHASH", passwordHash, dbType: DbType.Binary, direction: ParameterDirection.Input);
            parmeterPassword.Add("PASSALT", passwordSalt, dbType: DbType.Binary, direction: ParameterDirection.Input);

            var change = _dbContext.Connection.QueryAsync("LOGIN_PACKAGE.CHANAGEPASSOWRD", parmeterPassword, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
