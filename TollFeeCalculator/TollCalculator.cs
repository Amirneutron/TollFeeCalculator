using System;
using Nager.Date;


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

            if (dates.Length == 0 || vehicle.isTollFreeVehicle())
                return 0;

            //var sortDate = dates.OrderBy(date => date.Ticks).ToList();
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
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
        
        

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date)) return 0;
            

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
        
        
    }
}