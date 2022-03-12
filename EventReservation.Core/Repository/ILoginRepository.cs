using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Repository
{
    public interface ILoginRepository
    {
        List<Login> GetAllLogin();
        //Update Update()

        bool AddLogin(Login login);
        bool UpdateLogin(LoginToUpdateDTO loginToUpdate);

        bool DeleteLogin(int id);

        Login GetLoginById(int Id);
        Login Auth(Login login);




    }
}
