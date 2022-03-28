using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IHallService
    {
        Hall CreateHall(Hall hall);
        bool UpdateHall(Hall hall);
        bool DeleteHall(int id);
        List<Hall>GetAllHall();
        Hall GetHallById(int id);
        List<Hall>GetHallByName(string name);
        Hall GetCheapestHall();
        List<Hall> GetHallByCapacity(int CAP);
        List<Hall> GetHallByPrice(int price);
        Location GetHallByLocationId(int id);
        List<Hall>GetHallByUsage(string usage);
    }
}
