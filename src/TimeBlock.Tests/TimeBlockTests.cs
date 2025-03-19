using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeBlock.Core;

namespace TimeBlock.Tests
{
    [TestClass]
    public class TimeBlockTests
    {
        [TestMethod]
        [DataRow("2000-01-01 00:00:00", true, "* * * * * *")]                     // All wildcards
        [DataRow("1999-12-31 23:59:59", true, "* * * * * *")]                     // All wildcards, edge of year
        [DataRow("2024-02-29 00:00:00", true, "* * * * * *")]                     // Leap year

        // Specific time matches
        [DataRow("2024-03-15 14:30:45", true, "45 30 14 * * *")]                  // Exact time match
        [DataRow("2024-03-15 14:30:46", false, "45 30 14 * * *")]                 // One second off

        // Range matches
        [DataRow("2024-01-01 09:00:00", true, "* * 9-17 * * *")]                  // Business hours start
        [DataRow("2024-01-01 17:59:59", true, "* * 9-17 * * *")]                  // Business hours end
        [DataRow("2024-01-01 08:59:59", false, "* * 9-17 * * *")]                 // Just before business hours

        // Day of week matches
        [DataRow("2024-03-18 12:00:00", true, "* * * Mon * *")]                   // Monday
        [DataRow("2024-03-19 12:00:00", false, "* * * Mon * *")]                  // Tuesday
        [DataRow("2024-03-18 12:00:00", true, "* * * Mon-Fri * *")]              // Weekday
        [DataRow("2024-03-23 12:00:00", false, "* * * Mon-Fri * *")]             // Saturday

        // Month matches
        [DataRow("2024-01-15 12:00:00", true, "* * * * Jan,Jun,Dec *")]          // January
        [DataRow("2024-02-15 12:00:00", false, "* * * * Jan,Jun,Dec *")]         // February
        [DataRow("2024-06-15 12:00:00", true, "* * * * Jan,Jun,Dec *")]          // June

        // Year ranges
        [DataRow("2020-01-01 00:00:00", true, "* * * * * 2020-2025")]            // Start of year range
        [DataRow("2025-12-31 23:59:59", true, "* * * * * 2020-2025")]            // End of year range
        [DataRow("2019-12-31 23:59:59", false, "* * * * * 2020-2025")]           // Just before year range
        [DataRow("2026-01-01 00:00:00", false, "* * * * * 2020-2025")]           // Just after year range

        // Complex patterns
        [DataRow("2024-03-18 09:00:00", true, "0 0 9-17 Mon-Fri Mar-Nov 2024")]  // Business hours on weekday in specific months
        [DataRow("2024-03-18 08:59:59", false, "0 0 9-17 Mon-Fri Mar-Nov 2024")] // Just before business hours
        [DataRow("2024-03-23 12:00:00", false, "0 0 9-17 Mon-Fri Mar-Nov 2024")] // Weekend
        [DataRow("2024-12-18 12:00:00", false, "0 0 9-17 Mon-Fri Mar-Nov 2024")] // Outside month range

        // Specific combinations
        [DataRow("2024-01-01 12:30:00", true, "0 30 12 * Jan,Mar,May,Jul 2024")] // Noon:30 in specific months
        [DataRow("2024-02-01 12:30:00", false, "0 30 12 * Jan,Mar,May,Jul 2024")] // Wrong month
        [DataRow("2024-01-01 12:31:00", false, "0 30 12 * Jan,Mar,May,Jul 2024")] // Wrong minute

        // Edge cases
        [DataRow("2024-12-31 23:59:59", true, "59 59 23 * * *")]                 // Last second of day
        [DataRow("2024-01-01 00:00:00", true, "0 0 0 * * *")]                    // First second of day
        public void IsMatch(string dateTimeStr, bool expected, string spec)
        {
            // Convert string to DateTime
            var dateTime = DateTime.Parse(dateTimeStr);

            Core.TimeSpecification timeBlock = TimeSpecificationParser.Parse(spec);
            bool actual = timeBlock.IsMatch(dateTime);

            Assert.AreEqual(expected, actual);
        }
    }
}