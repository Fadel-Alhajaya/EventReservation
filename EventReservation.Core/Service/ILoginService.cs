using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface ILoginService
    {
        bool AddLogin(Login login);
        bool DeleteLogin(int id);
        List<Login> GetAllLogin();
        Login GetLoginById(int Id);
        bool UpdateLogin(LoginToUpdateDTO loginToUpdate);
        Login Auth(Login login);
    }
}
