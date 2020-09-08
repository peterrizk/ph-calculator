using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicHoliday.Calculator.Source.Db.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicHoliday.Calculator.Source.Db.Configuration
{
    public static class DbSetup
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = configuration.GetSection("Database").Get<DatabaseOptions>();

            var server = dbOptions.Server;
            var port = dbOptions.Port;
            var user = dbOptions.User;
            var password = dbOptions.Password;
            var database = dbOptions.DatabaseName;

            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

        }
    }
}
