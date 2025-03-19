using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeBlock.Core;

namespace TimeBlock.Tests
{
    [TestClass]
    public class TimeBlockParserTests
    {
        // Test data as static properties
        private static object[] AllWildcards => new object[]
        {
            "* * * * * *",
            true,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value
        };

        private static object[] IntervalPattern => new object[]
        {
            "/5 * * * * *",
            true,
            IntervalUnit.From(5),
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            AlwaysUnit.Value
        };

        private static object[] SingleValues => new object[]
        {
            "5 10 20 Sun Dec 1995",
            true,
            SingleUnit.From(5),
            SingleUnit.From(10),
            SingleUnit.From(20),
            SingleUnit.From((int)DayOfWeek.Sunday),
            SingleUnit.From(11),
            SingleUnit.From(1995)
        };

        private static object[] MixedRangeAndSpecific => new object[]
        {
            "30-45 * * Mon,Tue,Fri * *",
            true,
            RangeUnit.From(30, 45),
            AlwaysUnit.Value,
            AlwaysUnit.Value,
            SpecificUnit.From(1, 2, 5),
            AlwaysUnit.Value,
            AlwaysUnit.Value
        };

        private static object[] AllRanges => new object[]
        {
            "0-10 5-10 7-9 Sun-Fri Jan-Jun 1990-1993",
            true,
            RangeUnit.From(0, 10),
            RangeUnit.From(5, 10),
            RangeUnit.From(7, 9),
            RangeUnit.From(0, 5),
            RangeUnit.From(0, 5),
            RangeUnit.From(1990, 1993)
        };

        private static object[] AllSpecific => new object[]
        {
            "4,8,10 12,24,36 4,8,23 Tue,Thu,Fri Jan,Apr,Dec 1994,2001,2024,2030",
            true,
            SpecificUnit.From(4, 8, 10),
            SpecificUnit.From(12, 24, 36),
            SpecificUnit.From(4, 8, 23),
            SpecificUnit.From((int)DayOfWeek.Tuesday, (int)DayOfWeek.Thursday, (int)DayOfWeek.Friday),
            SpecificUnit.From(0, 3, 11),
            SpecificUnit.From(1994, 2001, 2024, 2030)
        };

        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void TryParse(string spec, bool expected, IUnit seconds, IUnit minutes, IUnit hours, IUnit days, IUnit months, IUnit years)
        {
            bool success = TimeSpecificationParser.TryParse(spec, out Core.TimeSpecification timeBlock);
            Assert.AreEqual(expected, success);

            Assert.AreEqual(seconds, timeBlock.Seconds);
            Assert.AreEqual(minutes, timeBlock.Minutes);
            Assert.AreEqual(hours, timeBlock.Hours);
            Assert.AreEqual(days, timeBlock.Days);
            Assert.AreEqual(months, timeBlock.Months);
            Assert.AreEqual(years, timeBlock.Years);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return AllWildcards;
            yield return IntervalPattern;
            yield return SingleValues;
            yield return MixedRangeAndSpecific;
            yield return AllRanges;
            yield return AllSpecific;
        }
    }
}