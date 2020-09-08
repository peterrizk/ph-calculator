using System;
using System.Collections.Generic;
using System.Text;

namespace PublicHoliday.Calculator.Source.Db.Configuration
{
    public class DatabaseOptions
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }
}
