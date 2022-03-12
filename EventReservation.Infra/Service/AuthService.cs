using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.DTO.UserDto;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Service
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _confing;
        public AuthService(IAuthRepository authRepository,IConfiguration configuration)
        {
            _confing = configuration;
            _authRepository = authRepository;
        }

        public Task<bool> ChangePassword(PasswordToDto passwordToDto)
        {
            return _authRepository.ChangePassword(passwordToDto);
        }

        public Task<bool> EmailExsists(string email)
        {
            return _authRepository.EmailExsists(email);
        }

        public string Login(UserToLoginDto userToLoginDto)
        {
            var result = _authRepository.Login(userToLoginDto);

            if (result.IsFaulted || result.Result==null) return "erorr";
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,result.Result.UserId.ToString()),
                new Claim(ClaimTypes.Sid,result.Result.Loginid.ToString()),
                new Claim(ClaimTypes.Name,result.Result.Username),
                new Claim(ClaimTypes.Role,result.Result.Position),
                new Claim(ClaimTypes.Email,result.Result.Email),
                new Claim(ClaimTypes.GivenName,result.Result.Fullname)


            };
            // Token key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confing.GetSection("AppSettings:Token").Value));
            //Signing Credintials
            var creds =new  SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),//Change THIS TO 1 
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return  tokenHandler.WriteToken(token);

        }

        public string Register(UserToRegisterDto userToRegisterDto)
        {
            var result = _authRepository.Register(userToRegisterDto).Result;
            var claims = new[]
              {
                new Claim(ClaimTypes.NameIdentifier,result.UserId.ToString()),
                new Claim(ClaimTypes.Sid,result.Loginid.ToString()),
                new Claim(ClaimTypes.Name,result.Username),
                new Claim(ClaimTypes.Role,result.Position),
                new Claim(ClaimTypes.Email,result.Email),
                new Claim(ClaimTypes.GivenName,result.Fullname)


            };
            // Token key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confing.GetSection("AppSettings:Token").Value));
            //Signing Credintials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public Task<bool> UserNameExsists(string username)
        {
            return _authRepository.UserNameExsists(username);
        }
    }
}
