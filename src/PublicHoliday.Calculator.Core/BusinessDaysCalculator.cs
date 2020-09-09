using System;
using System.Collections.Generic;
using System.Text;

namespace PublicHoliday.Calculator.Core
{
    public class BusinessDaysCalculator
    {
        private readonly IPublicHolidayDatasource datasource;

        public BusinessDaysCalculator(IPublicHolidayDatasource datasource)
        {
            this.datasource = datasource;
        }

        public int count(DateTime start, DateTime end)
        {


            return 0;
        }
    }
}
