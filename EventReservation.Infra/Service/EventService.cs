using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
   public  class EventService: IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository= eventRepository;
        }

        public bool AcceptEvent(int EventId)
        {
            return _eventRepository.AcceptEvent(EventId);
        }

        public bool AddNewEvent(EventToAddDto eventToAddDto)
        {
            return _eventRepository.AddNewEvent(eventToAddDto);
        }

        public bool DeleteEvent(int EventId)
        {
            return _eventRepository.DeleteEvent(EventId);
        }

        public List<Event> GetAllAccepted()
        {
            return _eventRepository.GetAllAccepted();
        }

        public List<Event> GetAllEvent()
        {
            return _eventRepository.GetAllEvent();
        }

        public List<Event> GetAllRejected()
        {
            return _eventRepository.GetAllRejected();
        }

        public List<Event> GetEventByHall(int hallid)
        {
            return _eventRepository.GetEventByHall(hallid);
        }

        public Event GetEventById(int EventId)
        {
            return _eventRepository.GetEventById(EventId);
        }

        public bool GetStatusOfHall(int Hallid, DateTime startAt, DateTime EndAt)
        {
            return _eventRepository.GetStatusOfHall(Hallid,startAt,EndAt);
        }

        public bool GetStatusoFHall(DateTime startAt, DateTime EndAt, int hallid)
        {
            return _eventRepository.GetStatusOfHall(hallid, startAt, EndAt);
        }

        public bool RejectEvent(int EventId)
        {
            return _eventRepository.RejectEvent(EventId);
        }

        public List<Event> SearchBetweenDate(DateTime startAt, DateTime EndAt)
        {
            return _eventRepository.SearchBetweenDate(startAt, EndAt);
        }
    }
}
