using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Core
{
    public interface IPublicHolidayDatasource
    {
        IAsyncEnumerable<ZonedDateTime> List(int year);
    }
}
