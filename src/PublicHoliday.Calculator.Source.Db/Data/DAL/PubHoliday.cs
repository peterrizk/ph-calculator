using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Source.Db.Data.DAL
{
    public class PubHoliday
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public DayOfWeek DayOfTheWeek { get; set; }
        /// <summary>
        /// first monday(1) or second monday(2) etc.
        /// </summary>
        public int InstanceOfDay{ get; set; } 
        public HolidayType TheType { get; set; }
    }

    public enum HolidayType
    {
        Explicit = 1,
        WeekDay = 2,
        DynamicRule=3
    }
}
