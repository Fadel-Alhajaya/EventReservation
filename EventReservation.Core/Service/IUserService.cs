using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Service
{
    public interface IUserService
    {
        Task<List<UsertoResultDto>> GetAllUsers();


        Task<bool> AddAdmin(AddToAdminDto addToAdminDto);

        Task<bool> EditProfile(UserToUpdateDto userToUpdateDto);

        Task<bool> DeleteUser(int id);

        Task<User> GetUserById(int id);

        Task<List<UsertoResultDto>> SearchUser(UserToSearchDto userSearchDto);

        Task<bool> EmailExsists(string email);

        Task<bool> UserNameExsists(string username);




    }
}
