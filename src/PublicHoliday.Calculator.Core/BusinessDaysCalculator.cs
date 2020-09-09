using Microsoft.Extensions.Logging;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Core
{
    public class BusinessDaysCalculator
    {
        private readonly IPublicHolidayDatasource datasource;
        private readonly ILogger<BusinessDaysCalculator> logger;

        public BusinessDaysCalculator(IPublicHolidayDatasource datasource
            ,ILogger<BusinessDaysCalculator> logger)
        {
            this.datasource = datasource;
            this.logger = logger;
        }

        public async Task<int> count(DateTime start, DateTime end)
        {
            IAsyncEnumerable<ZonedDateTime> publicHolidays;
            try
            {
                publicHolidays = datasource.List(start.Year);
            }catch(Exception e)
            {
                logger.LogError(e, "Failed to retrieve public holidays");
                throw;
            }

            var curr = start.ToUniversalTime().CastToZonedDateTimeFromUtc(Zone.Sydney);
            var stopDate = end.ToUniversalTime().CastToZonedDateTimeFromUtc(Zone.Sydney);

            var count = 0;
            while(curr.Date < stopDate.Date)
            {
                curr = curr.PlusHours(24);
                if (curr.DayOfWeek == IsoDayOfWeek.Saturday || curr.DayOfWeek == IsoDayOfWeek.Sunday)
                    continue;

                var isPublicHoliday = false;
                await foreach(var hol in publicHolidays)
                {
                    if (hol.Date == curr.Date)
                        isPublicHoliday = true;
                }
                if (!isPublicHoliday) count++;
            }

            return count > 0? --count : 0;
        }

    }
}
