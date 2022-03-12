using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class LocationService : ILoctationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public bool DeleteLocation(int locationId)
        {
            return _locationRepository.DeleteLocation(locationId);
        }

        public List<Location> GetAllLocation()
        {
            return _locationRepository.GetAllLocation();
        }

        public Location GetLocationById(int locationId)
        {
            return _locationRepository.GetLocationById(locationId);
        }

        public Location GetlocationIdByAddress(string locationLan, string locationLat)
        {
            return _locationRepository.GetlocationIdByAddress(locationLan, locationLat);
        }

        public Location GetlocationIdByCity(string city)
        {
            return _locationRepository.GetlocationIdByCity(city);
        }

        public bool SetLocation(Location location)
        {
            return _locationRepository.SetLocation(location);
        }

        public bool UpdateLocation(Location location)
        {
            return _locationRepository.UpdateLocation(location);
        }
    }
}
