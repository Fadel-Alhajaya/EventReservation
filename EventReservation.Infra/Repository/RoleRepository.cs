using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Core.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IDbContext DbContext;
        public RoleRepository(IDbContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public List<Role> GetAllRoles()
        {


            IEnumerable<Role> result = DbContext.Connection.Query<Role>("Role_F_PACKAGE.GETALLROLE", commandType: CommandType.StoredProcedure);

            return result.ToList();


        }


       public bool AddRole(Role role)
        {
            var p = new DynamicParameters();
            p.Add("Pos", role.Position, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = DbContext.Connection.ExecuteAsync("Role_F_PACKAGE.CREATETROLE", p, commandType: CommandType.StoredProcedure);
            if(result==null) return false;
            return true;
            
        }

        public bool UpdateRole(RoleToUpdateDTO Rolto)
        {
            var p = new DynamicParameters();
            p.Add("Rollid", Rolto.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Pos", Rolto.Position, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = DbContext.Connection.ExecuteAsync("Role_F_PACKAGE.UPDATEROLE", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }



        public bool DeleteRole(int id)
        {


            var p = new DynamicParameters();
            p.Add("Rollid", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.ExecuteAsync("Role_F_PACKAGE.DELETEROLE", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return  true ;
           
        }

        public Role GetRoleById(int id)
        {

            var p = new DynamicParameters();
            p.Add("id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.QueryFirstOrDefault<Role>("Role_F_PACKAGE.GETROLEBYID", p,commandType: CommandType.StoredProcedure);

            return result;


        }




    }
}
