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
    public class EventRepository : IEventRepository
    {
        private readonly IDbContext _dbContext;
        public EventRepository(IDbContext DbContext)
        {
            _dbContext = DbContext;
        }



        public Event AcceptEvent(int EventId)
        {
            var p = new DynamicParameters();
            p.Add("Id", EventId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Event>("Event_PACKAGE.AcceptEvent", p, commandType: CommandType.StoredProcedure);
            if (result.Status !="Accepted")
                return null;
            return result;
        }

        public bool AddNewEvent(EventToAddDto eventToAddDto)
        { //edited
            var p = new DynamicParameters();
            p.Add("ETYPE", eventToAddDto.Eventtype, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("START_DATE", eventToAddDto.Startdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("END_DATE", eventToAddDto.Enddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("ESTATUS", "Pending", dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("hall", eventToAddDto.HallId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("user", eventToAddDto.UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("NumberPerson", eventToAddDto.NoPerson, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("totprice", eventToAddDto.totalprice, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("cardtoken", eventToAddDto.cardtokenid, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.ExecuteAsync("Event_PACKAGE.CREATEEvent", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return false;
            return true;
        }

        public bool DeleteEvent(int EventId)
        {
            var p = new DynamicParameters();
            p.Add("ID", EventId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.ExecuteAsync("Event_PACKAGE.DELETEEvent", p, commandType: CommandType.StoredProcedure);
            if (result== null)
                return false;
            return true;
        }

        public List<Event> GetAllAccepted()
        {
            var result = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventAccepted", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public List<EventInfoDTO> GetAllEvent()
        {
            var result = _dbContext.Connection.Query<EventInfoDTO>("Event_PACKAGE.GETALLEvent", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public List<Event> GetAllRejected()
        {
            var result = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventRejected", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public EventResultToDto GetEventById(int EventId)
        {
            var p = new DynamicParameters();
            p.Add("Id", EventId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<EventResultToDto>("Event_PACKAGE.GetEventById", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }


        public Event RejectEvent(int EventId)
        {
            var p = new DynamicParameters();
            p.Add("Id", EventId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Event>("Event_PACKAGE.RejectEvent", p, commandType: CommandType.StoredProcedure);
            if (result.Status != "Rejected")
                return null;
            return result;
        }

       

        public List<Event> SearchBetweenDate(DateTime startAt, DateTime EndAt)
        {
            var p = new DynamicParameters();
            p.Add("StartAt", startAt, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("END_DATE", EndAt, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.Query<Event>("Event_PACKAGE.SearchBetweenDates", p, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

       public  List<Event> GetEventByHall(int hallid)
        {
            var p = new DynamicParameters();
            p.Add("hall", hallid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            
            var result = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventByHall", p, commandType: CommandType.StoredProcedure);

            return result.ToList();

        }

        public bool GetStatusOfHall(int Hallid, DateTime startAt, DateTime EndAt)
        {
            var p = new DynamicParameters();
            p.Add("StartAt", startAt, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("END_DATE", EndAt, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("hall", Hallid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Event>("Event_PACKAGE.GetEventByDate", p, commandType: CommandType.StoredProcedure);
            if (result != null)
            {
                return false;//can't reseve because there a event in hall
            }

            return true;
        }

       
    }
}
