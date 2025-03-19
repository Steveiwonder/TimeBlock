using System;
using System.Collections.Generic;

namespace TimeBlock.Core
{
    public class TimePatternBuilder
    {
        private IUnit _seconds = AlwaysUnit.Value;
        private IUnit _minutes = AlwaysUnit.Value;
        private IUnit _hours = AlwaysUnit.Value;
        private IUnit _days = AlwaysUnit.Value;
        private IUnit _months = AlwaysUnit.Value;
        private IUnit _years = AlwaysUnit.Value;

        public TimePatternBuilder WithSeconds(string pattern)
        {
            _seconds = ParseUnit(pattern);
            return this;
        }

        public TimePatternBuilder WithMinutes(string pattern)
        {
            _minutes = ParseUnit(pattern);
            return this;
        }

        public TimePatternBuilder WithHours(string pattern)
        {
            _hours = ParseUnit(pattern);
            return this;
        }

        public TimePatternBuilder WithDays(string pattern)
        {
            _days = ParseUnit(pattern);
            return this;
        }

        public TimePatternBuilder WithMonths(string pattern)
        {
            _months = ParseUnit(pattern);
            return this;
        }

        public TimePatternBuilder WithYears(string pattern)
        {
            _years = ParseUnit(pattern);
            return this;
        }

        private IUnit ParseUnit(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException(nameof(pattern));

            if (pattern == "*")
                return AlwaysUnit.Value;

            if (pattern.Contains("-"))
            {
                var parts = pattern.Split('-');
                if (parts.Length != 2 || !int.TryParse(parts[0], out int lower) || !int.TryParse(parts[1], out int upper))
                    throw new ArgumentException($"Invalid range format: {pattern}");
                return new RangeUnit(upper, lower);
            }

            if (pattern.Contains(","))
            {
                var parts = pattern.Split(',');
                var values = new List<int>();
                foreach (var part in parts)
                {
                    if (!int.TryParse(part, out int value))
                        throw new ArgumentException($"Invalid value in list: {part}");
                    values.Add(value);
                }
                return new SpecificUnit(values.ToArray());
            }

            // Handle single value
            if (int.TryParse(pattern, out int singleValue))
                return new SingleUnit(singleValue);

            throw new ArgumentException($"Invalid pattern format: {pattern}");
        }

        public TimeSpecification Build()
        {
            return new TimeSpecification(
                _seconds,
                _minutes,
                _hours,
                _days,
                _months,
                _years
            );
        }

        public static TimePatternBuilder Create()
        {
            return new TimePatternBuilder();
        }
    }
}