using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
   public class HallService: IHallService
    {
        private readonly IHallRepository _hallRepository;
        public HallService(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public bool CreateHall(Hall hall)
        {
            return _hallRepository.CreateHall(hall);
        }

        public bool DeleteHall(int id)
        {
            return _hallRepository.DeleteHall(id);
        }

        public List<Hall> GetAllHall()
        {
            return _hallRepository.GetAllHall();
        }

        public Hall GetCheapestHall()
        {
            return _hallRepository.GetCheapestHall();
        }

        public List<Hall> GetHallByCapacity(int CAP)
        {
            return _hallRepository.GetHallByCapacity(CAP);
        }

        public Hall GetHallById(int id)
        {
            return _hallRepository.GetHallById(id);
        }

        public Hall GetHallByLocationId(int id)
        {
            return _hallRepository.GetHallByLocationId(id);
        }

        public List<Hall> GetHallByName(Hall hall)
        {
            return _hallRepository.GetHallByName(hall);
        }

        public List<Hall> GetHallByUsage(string usage)
        {
            return _hallRepository.GetHallByUsage(usage);
        }

        public bool UpdateHall(Hall hall)
        {
            return _hallRepository.UpdateHall(hall);
        }
    }
}
