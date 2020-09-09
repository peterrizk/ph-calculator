using Microsoft.Extensions.DependencyInjection;
using PublicHoliday.Calculator.Core;
using PublicHoliday.Calculator.Source.Db.BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicHoliday.Calculator.Source.Db.Configuration
{
    public static class DatasourceDI
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddScoped<IPublicHolidayDatasource, DatabasePublicHolidays>();
        }
    }
}