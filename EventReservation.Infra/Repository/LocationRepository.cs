using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Core.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IDbContext _dbContext;
        public LocationRepository(IDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public bool DeleteLocation(int locationId)
        {
            var p = new DynamicParameters();
            p.Add("ID", locationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.ExecuteAsync("Location_PACKAGE.DELETELocation", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return false;
            return true;
        }

        public List<Location> GetAllLocation()
        {
            var result = _dbContext.Connection.Query<Location>("Location_PACKAGE.GETALLLocation", commandType: CommandType.StoredProcedure);

            return result.ToList() ;
        }

        public Location GetLocationById(int locationId)
        {
            var p = new DynamicParameters();
            p.Add("Id", locationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Location>("Location_PACKAGE.GetLocationById", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public Location GetlocationIdByAddress(string locationLan, string locationLat)
        {
            var p = new DynamicParameters();
            p.Add("LONGITUDENum", locationLan, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LATITUDENum", locationLat, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Location>("Location_PACKAGE.GetLocationByAdress", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public Location GetlocationIdByCity(string city)
        {
            var p = new DynamicParameters();
            p.Add("cityname", city, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Location>("Location_PACKAGE.GetLocationByCityName", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public Location SetLocation(Location location)
        {
           
            var p = new DynamicParameters();
            p.Add("cityname", location.City, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("countryname", location.Country, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LATITUDE", location.Latitude.ToString(), dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LONGITUDE", location.Longitude.ToString(), dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.QueryFirstOrDefault<Location>("Location_PACKAGE.CREATELocation", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public bool UpdateLocation(Location location)
        {
            var p = new DynamicParameters();
            p.Add("Id", location.Locationid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("cityname", location.City, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("countryname", location.Country, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LATITUDE", location.Latitude.ToString(), dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LONGITUDE", location.Longitude.ToString(), dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.ExecuteAsync("Location_PACKAGE.CREATELocation", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return false;
            return true;
        }
    }
}
