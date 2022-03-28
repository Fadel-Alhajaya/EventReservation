using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IEventService
    {
        List<EventInfoDTO> GetAllEvent();
        List<Event> GetAllAccepted();
        List<Event> GetAllRejected();
        bool AddNewEvent(EventToAddDto eventToAddDto);
        Event AcceptEvent(int EventId);
        Event RejectEvent(int EventId);
        bool DeleteEvent(int EventId);

        bool GetStatusOfHall(int Hallid, DateTime startAt, DateTime EndAt);
        //Event GetEventHall(int HallId);
        EventResultToDto GetEventById(int EventId);
        bool GetStatusoFHall(DateTime startAt, DateTime EndAt, int hallid);
        List<Event> SearchBetweenDate(DateTime startAt, DateTime EndAt);
        List<Event> GetEventByHall(int hallid);
    }
}
