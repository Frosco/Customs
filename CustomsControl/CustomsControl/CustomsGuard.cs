using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomsControl
{
    public enum WeightClass { light, heavy, truck }
    public static class CustomsGuard
    {
        public static int WeightClassBorder { get; private set; } // Under this weight the vehicle is classified as "light", above it is classified "heavy"
        public static Dictionary<WeightClass,int> WeightClassFees { get; private set; }
        public static double MotorcycleFactor { get; private set; }
        public static double HolidayFactor { get; private set; }
        public static double NightFactor { get; private set; }
        public static HashSet<DateTime> Holidays { get; private set; }

        static CustomsGuard()
        {
            WeightClassBorder = 1000;
            WeightClassFees = new Dictionary<WeightClass, int>()
            {
                { WeightClass.light, 500 },
                { WeightClass.heavy, 1000 },
                { WeightClass.truck, 2000 }
            };

            MotorcycleFactor = 0.7;
            HolidayFactor = 2;
            NightFactor = 0.5;

            Holidays = GetHolidays(DateTime.Now);
        }

        public static double GetFee(Vehicle vehicle, DateTime moment)
        {
            if (vehicle.Environmental)
                return 0;

            double fee = 0;

            if (vehicle.Type == vehicleType.truck)
                fee = WeightClassFees[WeightClass.truck];
            else
            {
                if (vehicle.Weight < WeightClassBorder)
                    fee = WeightClassFees[WeightClass.light];
                else
                    fee = WeightClassFees[WeightClass.heavy];

                if (vehicle.Type == vehicleType.motorcycle)
                    fee *= MotorcycleFactor;
            }

            if (CheckForHoliday(moment))
                fee *= HolidayFactor;
            else if (moment.Hour > 17 || moment.Hour < 6)
                fee *= NightFactor;

            return Math.Round(fee, 2);
        }

        public static double GetFee(Vehicle vehicle)
        {
            var moment = DateTime.Now;
            return GetFee(vehicle, moment);
        }
        

        private static bool CheckForHoliday(DateTime moment)
        {
            if (moment.DayOfWeek == DayOfWeek.Saturday || moment.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            HashSet<DateTime> holidays;

            if (moment.Year != DateTime.Now.Year)
                holidays = GetHolidays(moment);
            else
                holidays = Holidays;

            return holidays.Contains(moment.Date);
        }

        private static HashSet<DateTime> GetHolidays(DateTime moment)
        {
            var date = moment.Date;
            var holidays = new HashSet<DateTime>();

            var easterSunday = EasterSunday(date.Year);
            var midsummerEvening = MidsummerEvening(date.Year);

            //Nyårsdagen
            holidays.Add(new DateTime(date.Year, 1, 1));
            //Trettondedag jul
            holidays.Add(new DateTime(date.Year, 1, 6));
            //Långfredagen
            holidays.Add(easterSunday.AddDays(-2));
            //Annandag påsk
            holidays.Add(easterSunday.AddDays(1));
            //Första maj
            holidays.Add(new DateTime(date.Year, 5, 1));
            // Kristi himmelfärdsdag
            holidays.Add(easterSunday.AddDays(40));
            // Sveriges nationaldag
            holidays.Add(new DateTime(date.Year, 6, 6));
            // Midsommarafton
            holidays.Add(midsummerEvening);
            // Julafton
            holidays.Add(new DateTime(date.Year, 12, 24));
            // Juldagen
            holidays.Add(new DateTime(date.Year, 12, 25));
            // Annandag jul
            holidays.Add(new DateTime(date.Year, 12, 26));
            // Nyårsafton
            holidays.Add(new DateTime(date.Year, 12, 31));

            return holidays;
        }

        private static DateTime MidsummerEvening(int year)
        {
            DateTime date = new DateTime(0);

            for (int i = 0; i < 7; i++)
            {
                date = new DateTime(year, 6, 19 + i);
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    break;
                }
            };

            return date;
        }

        // Algorithm source: https://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        private static DateTime EasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int lunarCyclePosition = year % 19;
            int century = year / 100;
            // Number of days between equinox and the next full moon
            int toFullMoon = (century - century / 4 - (8 * century + 13) / 25 + 19 * lunarCyclePosition + 15) % 30;
            // First Sunday after the named full moon
            int toSunday = toFullMoon - toFullMoon / 28 * (1 - toFullMoon / 28 * (29 / (toFullMoon + 1)) * ((21 - lunarCyclePosition) / 11));

            day = toSunday - ((year + year / 4 + toSunday + 2 - century + century / 4) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day); ;
        }
    }
}
