using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using PublicHoliday.Calculator.Core;
using PublicHoliday.Calculator.Source.Db.Data;
using PublicHoliday.Calculator.Source.Db.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Source.Db.BLL
{
    public class DatabasePublicHolidays : IPublicHolidayDatasource
    {
        private readonly DataContext dataContext;

        public DatabasePublicHolidays(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<IEnumerable<Instant>> List(int year)
        {
            throw new NotImplementedException();
        }

        private async IAsyncEnumerable<ZonedDateTime> Compute(int year)
        {
            await foreach (var holiday in dataContext.Holidays.AsAsyncEnumerable())
            {
                yield return Compute(year,holiday);
            }
        }

        private ZonedDateTime Compute(int year, PubHoliday holiday)
        {
            return holiday.TheType switch
            {
                HolidayType.Explicit => CalculateExplicitType(year, holiday),
                HolidayType.WeekDay => CalculateWeekDayType(year, holiday),
                HolidayType.DynamicRule 
                            => CalculateDynamicType(holiday.DayOfTheWeek, holiday.InstanceOfDay, holiday.Month, year),
                _ => throw new ArgumentException($"bad type {holiday.TheType}")
            };
        }

        private ZonedDateTime CalculateExplicitType(int year, PubHoliday holiday)
        {
            return new DateTime(year, holiday.Month, holiday.Day).CastToZonedDateTimeFromUtc(Zone.Sydney);
        }

        private ZonedDateTime CalculateWeekDayType(int year, PubHoliday holiday)
        {
            var conversion = CalculateExplicitType(year, holiday);
            return conversion.DayOfWeek switch
            {
                IsoDayOfWeek.Saturday => conversion.Plus(Duration.FromDays(2)),
                IsoDayOfWeek.Sunday => conversion.Plus(Duration.FromDays(1)),
                _ => conversion,
            };
        }

        private ZonedDateTime CalculateDynamicType(IsoDayOfWeek day, int instanceOfDay, int month, int year)
        {
            var instance = CalculateExplicitType(year, new PubHoliday() { Day = 1, Month = month });
            var count = 0;
            while(instance.Month == month)
            {
                if (instance.DayOfWeek == day) count++;
                if (instanceOfDay == count) return instance;
                instance.PlusHours(24);
            }
            throw new Exception($"Public holiday not found day:{day} instanfceOfday:{instanceOfDay} month:{month} year:{year}");
        }

    }
}
