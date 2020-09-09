using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NodaTime;
using NSubstitute;
using PublicHoliday.Calculator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PublicHoliday.Calculator.UnitTests
{
    /// <summary>
    /// Very basic cases. TODO: If more time add more test cases of boundaries and error validation.
    /// </summary>
    public class CalculatorTests
    {
        IPublicHolidayDatasource mockDataSource;
        ILogger<BusinessDaysCalculator> mockLogger;
        public static async IAsyncEnumerable<ZonedDateTime> GetTestValues()
        {
            yield return new DateTime(2020,4,25).ToUniversalTime().CastToZonedDateTimeFromUtc(Zone.Sydney);
            yield return new DateTime(2020,1,1).ToUniversalTime().CastToZonedDateTimeFromUtc(Zone.Sydney);
            yield return new DateTime(2020,6,8).ToUniversalTime().CastToZonedDateTimeFromUtc(Zone.Sydney);

            await Task.CompletedTask; // to make the compiler warning go away
        }

        public CalculatorTests()
        {
            mockLogger = Substitute.For<ILogger<BusinessDaysCalculator>>();
            mockDataSource = Substitute.For<IPublicHolidayDatasource>();
            mockDataSource.List(Arg.Any<int>()).Returns(GetTestValues());
        }

        [Fact]
        public async Task Test_Explicit_PH()
        {
            // arrange
            var target = new BusinessDaysCalculator(mockDataSource, mockLogger);

            //act
            var count = await target.count(new DateTime(2020,4,20), new DateTime(2020,4,30));

            //assert
            Assert.Equal(7, count);
        }

        [Fact]
        public async Task Test_WeekDay_PH()
        {
            // arrange
            var target = new BusinessDaysCalculator(mockDataSource, mockLogger);

            //act
            var count = await target.count(new DateTime(2020, 4, 1), new DateTime(2020, 4, 8));

            //assert
            Assert.Equal(4, count);
        }

        [Fact]
        public async Task Test_Dynamic_PH()
        {
            // arrange
            var target = new BusinessDaysCalculator(mockDataSource, mockLogger);

            //act
            var count = await target.count(new DateTime(2020, 6, 1), new DateTime(2020, 6, 12));

            //assert
            Assert.Equal(7, count);
        }
    }
}
