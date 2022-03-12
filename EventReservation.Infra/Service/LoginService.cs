using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public bool AddLogin(Login login)
        {
            return _loginRepository.AddLogin(login);
        }

        public Login Auth(Login login)
        {
            return _loginRepository.Auth(login);
        }

        public bool DeleteLogin(int id)
        {
            return _loginRepository.DeleteLogin(id);
        }

        public List<Login> GetAllLogin()
        {
            return _loginRepository.GetAllLogin();
        }

        public Login GetLoginById(int Id)
        {
            return _loginRepository.GetLoginById(Id);
        }

        public bool UpdateLogin(LoginToUpdateDTO loginToUpdate)
        {
            return _loginRepository.UpdateLogin(loginToUpdate);
        }
    }
}
