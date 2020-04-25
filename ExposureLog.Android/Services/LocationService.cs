﻿using ExposureLog.Models;
using ExposureLog.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace ExposureLog.Droid.Services
{
    class LocationService : ILocationService
    {
        public async Task<Coordinates> GetCoordinatesAsync()
        {
            var location = await Geolocation.GetLocationAsync();
            return new Coordinates
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
    }
}