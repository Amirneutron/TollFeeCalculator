using System;

namespace TollFeeCalculator
{
    public class Vehicle : IVehicle
    {
        String _VehicleType;

        public Vehicle(string vehicleType)
        {
            _VehicleType = vehicleType;
        }

        public String GetVehicleType()
        {
            return _VehicleType;
        }
        
    }
}