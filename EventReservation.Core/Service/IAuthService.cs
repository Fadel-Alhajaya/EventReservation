using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Service
{
    public interface IAuthService
    {

        string Register(UserToRegisterDto userToRegisterDto);


        string Login(UserToLoginDto userToLoginDto);

        Task<bool> EmailExsists(string email);

        Task<bool> UserNameExsists(string username);

        Task<bool> ChangePassword(PasswordToDto passwordToDto);





    }
}
