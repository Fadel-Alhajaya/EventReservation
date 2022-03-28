using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
   public  interface ILoctationService
    {
        List<Location> GetAllLocation();
        Location SetLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(int locationId);
        Location GetLocationById(int locationId);
        Location GetlocationIdByAddress(string locationLan, string locationLat);
        Location GetlocationIdByCity(string city);
    
}
}
