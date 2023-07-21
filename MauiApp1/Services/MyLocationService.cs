using MauiApp1.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{

    public class MyLocationService : IMyLocationService
    {
        private readonly MobileDB _mobileDB;

        public MyLocationService(MobileDB mobileDB)
        {
            _mobileDB = mobileDB;
        }
        public async Task<MyLocation> AddMyLocation(double latitude, double longitude)
        {
            var loc = new MyLocation { Latitude = latitude, Longitude = longitude };
            _mobileDB.MyLocations.Add(loc);
            await _mobileDB.SaveChangesAsync();
            return loc;
        }
    }
}
