using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnsureThat;
using System.Globalization;

namespace PublicHoliday.Calculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This endpoint accepts a date range to calculate the number of business days based on Sydney time.
        /// </summary>
        /// <param name="EndDateExclusive">yyyy-MM-dd</param>
        /// <param name="StartDateExclusive">yyyy-MM-dd</param>
        /// <returns></returns>
        [HttpPost("CountBusinessDays")]
        public ActionResult CountBusinessDaysFromARange(string StartDateExclusive, string EndDateExclusive)
        {
            EnsureArg.HasValue(StartDateExclusive);
            EnsureArg.HasValue(EndDateExclusive);

            var start = DateTime.ParseExact(StartDateExclusive, "yyyy-MM-dd",CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(EndDateExclusive, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return Ok(StartDateExclusive);
        }
    }
}
