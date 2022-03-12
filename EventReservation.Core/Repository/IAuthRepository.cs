using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Repository
{
    public interface IAuthRepository
    {

        Task<UserToResultRegisterDto> Register(UserToRegisterDto userToRegisterDto);

        Task<LoginToDto> Login(UserToLoginDto userToLoginDto);

        Task<bool> ChangePassword(PasswordToDto passwordToDto);
     

        Task<bool> EmailExsists(string email);

        Task<bool> UserNameExsists(string username);






    }
}
