using TimeBlock.Core;

namespace TimeBlock.Tests
{

    public class TimeBlockParserTests
    {
        public static IEnumerable<object[]> TryParseData
        {
            get
            {
                yield return new object[] { "* * * * * *", true, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value };
                yield return new object[] { "/5 * * * * *", true, IntervalUnit.From(5), AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value, AlwaysUnit.Value };
                yield return new object[] { "5 10 20 Sun Dec 1995", true, SingleUnit.From(5), SingleUnit.From(10), SingleUnit.From(20), SingleUnit.From((int)DayOfWeek.Sunday), SingleUnit.From(11), SingleUnit.From(1995) };
                yield return new object[] { "30-45 * * Mon,Tue,Fri * *", true, RangeUnit.From(30, 45), AlwaysUnit.Value, AlwaysUnit.Value, SpecificUnit.From(1, 2, 5), AlwaysUnit.Value, AlwaysUnit.Value };
                yield return new object[] { "0-10 5-10 7-9 Sun-Fri Jan-Jun 1990-1993", true, RangeUnit.From(0, 10), RangeUnit.From(5, 10), RangeUnit.From(7, 9), RangeUnit.From(0, 5), RangeUnit.From(0, 5), RangeUnit.From(1990, 1993) };
                yield return new object[] { "4,8,10 12,24,36 4,8,23 Tue,Thu,Fri Jan,Apr,Dec 1994,2001,2024,2030", true, SpecificUnit.From(4, 8, 10), SpecificUnit.From(12, 24, 36), SpecificUnit.From(4, 8, 23), SpecificUnit.From((int)DayOfWeek.Tuesday, (int)DayOfWeek.Thursday, (int)DayOfWeek.Friday), SpecificUnit.From(0, 3, 11), SpecificUnit.From(1994, 2001, 2024, 2030) };
            }
        }

        [Theory]
        // S M D M Y
        [MemberData(nameof(TryParseData))]
        public void TryParse(string spec, bool expected, IUnit seconds, IUnit minutes, IUnit hours, IUnit days, IUnit months, IUnit years)
        {
            bool success = TimeBlockParser.TryParse(spec, out Core.TimeBlock timeBlock);
            Assert.Equal(expected, success);

            Assert.Equal(seconds, timeBlock.Seconds);
            Assert.Equal(minutes, timeBlock.Minutes);
            Assert.Equal(hours, timeBlock.Hours);
            Assert.Equal(days, timeBlock.Days);
            Assert.Equal(months, timeBlock.Months);
            Assert.Equal(years, timeBlock.Years);
        }
    }
}