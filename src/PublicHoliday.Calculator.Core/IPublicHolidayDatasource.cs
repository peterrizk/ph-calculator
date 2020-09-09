using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Core
{
    public interface IPublicHolidayDatasource
    {
        Task<IEnumerable<Instant>> List(int year);
    }
}
