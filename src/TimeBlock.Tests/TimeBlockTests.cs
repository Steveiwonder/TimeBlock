using TimeBlock.Core;

namespace TimeBlock.Tests
{

    public class TimeBlockTests
    {
        public static IEnumerable<object[]> IsMatchData
        {
            get
            {
                yield return new object[] { new DateTime(2000, 1, 1, 0, 0, 0), true, "* * * * * *" };
                yield return new object[] { new DateTime(1999, 1, 1, 0, 0, 0), true, "* * * * * *" };
                yield return new object[] { new DateTime(2050, 1, 1, 0, 0, 0), true, "* * * * * *" };
                yield return new object[] { new DateTime(2050, 1, 1, 0, 0, 0), false, "* * * * * 2000-2010" };
            }
        }

        [Theory]
        // S M D M Y
        [MemberData(nameof(IsMatchData))]
        public void IsMatch(DateTime dateTime, bool expected, string spec)
        {
            Core.TimeBlock timeBlock = TimeBlockParser.Parse(spec);
            bool actual = timeBlock.IsMatch(dateTime);

            Assert.Equal(expected, actual);
        }
    }
}