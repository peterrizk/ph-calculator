using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicHoliday.Calculator.Core
{
    public static class DateTimeUtility
    {
        public static ZonedDateTime CastToZonedDateTimeFromUtc(
          this DateTime utcDateTime,
          DateTimeZone zone)
        {
            return utcDateTime.CastToUtcZonedDateTimeFromUtc().WithZone(zone);
        }

        public static ZonedDateTime CastToUtcZonedDateTimeFromUtc(
        this DateTime utcDateTime)
        {
            return LocalDateTime.FromDateTime(utcDateTime).InZoneStrictly(DateTimeZone.Utc);
        }
    }

    public static class Zone
    {
        public static readonly DateTimeZone Sydney = DateTimeZoneProviders.Tzdb["Australia/Sydney"];
        public static readonly DateTimeZone Perth = DateTimeZoneProviders.Tzdb["Australia/Perth"];
        public static readonly DateTimeZone Utc = DateTimeZone.Utc;
    }
}
