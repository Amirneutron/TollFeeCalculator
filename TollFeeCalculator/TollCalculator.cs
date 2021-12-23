using System;
using System.Collections.Generic;
using System.Linq;
using Nager.Date;
using PublicHoliday;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        private const int MaxTollForOneDay = 60;
        
        /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
            if (dates == null) throw new ArgumentNullException(nameof(dates));
            
            if (!dates.Any())
                return 0;

            var sortDate = dates.OrderBy(date => date.Ticks).ToList();
            DateTime intervalStart = sortDate[0];
            int totalFee = 0;
            foreach (DateTime date in sortDate)
            {
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                long diffInMillis = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillis/1000/60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > MaxTollForOneDay) totalFee = MaxTollForOneDay;
            return totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();
            return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 9;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 16;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 22;
            else if (hour == 8 && minute >= 0 && minute < 29) return 16;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 9;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 16;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 22;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 16;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7) return true;

            if (date.Year != DateTime.Now.Year) throw new ArgumentOutOfRangeException($"Date {date} is not from this year");
            
            if (DateSystem.IsPublicHoliday(date, CountryCode.SE))
            {
                Console.WriteLine("Is public holiday");
                return true;
            }


            return false;
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}