using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Repository
{
    public interface IRoleRepository
    {

        List<Role> GetAllRoles();
        //Update Update()

        bool AddRole(Role role);
        bool UpdateRole(RoleToUpdateDTO Rolto);
       
        bool DeleteRole(int id);

        Role  GetRoleById(int Id);







    }
}
