using MauiApp1.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IMyLocationService
    {
        Task<MyLocation> AddMyLocation(double latitude, double longitude);
    }
}
