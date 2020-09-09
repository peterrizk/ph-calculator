using Microsoft.EntityFrameworkCore;

using PublicHoliday.Calculator.Source.Db.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Source.Db.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<PubHoliday> Holidays { get; set; }

    }
}
