using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicHoliday.Calculator.Source.Db.Data;
using PublicHoliday.Calculator.Source.Db.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Source.Db.SeedData
{
    public static class SeedData
    {
        public static void Populate(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void Seed(DataContext context)
        {
            context.Database.Migrate();
            if (!context.Holidays.Any())
            {
                context.AddRange(
                    new PubHoliday()
                    {
                        Day = 25,//Anzac Day
                        Month = 4,
                        TheType = HolidayType.Explicit
                    },
                    new PubHoliday()
                    {
                        Day = 1,//New Years Day
                        Month = 1,
                        TheType = HolidayType.WeekDay,
                    },
                    new PubHoliday()
                    {
                        DayOfTheWeek = DayOfWeek.Monday,//Queens Birthday
                        Month = 6,
                        InstanceOfDay = 2,
                        TheType = HolidayType.DynamicRule
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
