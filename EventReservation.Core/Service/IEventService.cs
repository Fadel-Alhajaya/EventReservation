using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IEventService
    {
        List<Event> GetAllEvent();
        List<Event> GetAllAccepted();
        List<Event> GetAllRejected();
        bool AddNewEvent(EventToAddDto eventToAddDto);
        bool AcceptEvent(int EventId);
        bool RejectEvent(int EventId);
        bool DeleteEvent(int EventId);

        bool GetStatusOfHall(int Hallid, DateTime startAt, DateTime EndAt);
        //Event GetEventHall(int HallId);
        Event GetEventById(int EventId);
        bool GetStatusoFHall(DateTime startAt, DateTime EndAt, int hallid);
        List<Event> SearchBetweenDate(DateTime startAt, DateTime EndAt);
        List<Event> GetEventByHall(int hallid);
    }
}
