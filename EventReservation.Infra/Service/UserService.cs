using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {


            _userRepository = userRepository;

        }


        public Task<bool> AddAdmin(AddToAdminDto addToAdminDto)
        {
            return _userRepository.AddAdmin(addToAdminDto);
        }

        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public Task<bool> EditProfile(UserToUpdateDto userToUpdateDto)
        {
            return _userRepository.EditProfile(userToUpdateDto);
        }

        public Task<bool> EmailExsists(string email)
        {
            return _userRepository.EmailExsists(email);
        }

        public Task<List<UsertoResultDto>> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Task<User> GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public Task<List<UsertoResultDto>> SearchUser(UserToSearchDto userSearchDto)
        {
            return _userRepository.SearchUser(userSearchDto);
        }

        public Task<bool> UserNameExsists(string username)
        {
            return _userRepository.UserNameExsists(username);
        }
    }
}
