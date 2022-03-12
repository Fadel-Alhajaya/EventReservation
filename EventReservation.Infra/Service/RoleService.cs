using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class RoleService: IRoleService
    {

        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public bool AddRole(Role role)//Done
        {
            return _roleRepository.AddRole(role);
        }

        public bool DeleteRole(int id)//Done
        {
            return _roleRepository.DeleteRole(id);
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public Role GetRoleById(int Id)
        {
            return _roleRepository.GetRoleById(Id);
        }

        public bool UpdateRole(RoleToUpdateDTO Rolto)
        {
            return _roleRepository.UpdateRole(Rolto);
        }
    }
}
