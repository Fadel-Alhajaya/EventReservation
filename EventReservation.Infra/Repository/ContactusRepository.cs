using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Infra.Repository
{
   public class ContactusRepository: IContactusRepository
    {
        private readonly IDbContext _dbContext;

        public ContactusRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public bool CreateContact(Contactus contact)
        {
            var parmeter = new DynamicParameters();

            parmeter.Add("NAME", contact.Personalname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("PHONE", contact.Phonenumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("SUB", contact.Subject, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("MES", contact.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WEBID", contact.Websiteid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.ExecuteAsync("CONTACTUS_F_PACKAGE.CREATECONTACTUS", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }

        public bool DeleteContact(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("CONTACTUS_F_PACKAGE.DELETECONTACTUS", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }

        public List<Contactus> GetAllContact()
        {
            IEnumerable<Contactus> result = _dbContext.Connection.Query<Contactus>("CONTACTUS_F_PACKAGE.GETALLCONTACTUS", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public Contactus GetContactById(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            IEnumerable<Contactus> result = _dbContext.Connection.Query<Contactus>("CONTACTUS_F_PACKAGE.GETCONTACTUSBYID", parmeter, commandType: CommandType.StoredProcedure);


            return result.FirstOrDefault();
        }
    }
}
