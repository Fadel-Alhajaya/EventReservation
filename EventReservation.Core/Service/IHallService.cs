using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IHallService
    {
        bool CreateHall(Hall hall);
        bool UpdateHall(Hall hall);
        bool DeleteHall(int id);
        List<Hall>GetAllHall();
        Hall GetHallById(int id);
        List<Hall>GetHallByName(Hall hall);
        Hall GetCheapestHall();
        List<Hall> GetHallByCapacity(int CAP);
        Hall GetHallByLocationId(int id);
        List<Hall>GetHallByUsage(string usage);
    }
}
