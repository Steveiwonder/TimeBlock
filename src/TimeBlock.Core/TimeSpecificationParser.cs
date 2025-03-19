using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TimeBlock.Tests")]
namespace TimeBlock.Core
{

    public class TimeSpecificationParser
    {

        private static Dictionary<string, int> MonthNumberMapping = new Dictionary<string, int>(){
            {"Jan", 0},
            {"Feb", 1},
            {"Mar", 2},
            {"Apr", 3},
            {"May", 4},
            {"Jun", 5},
            {"Jul", 6},
            {"Aug", 7},
            {"Sept", 8},
            {"Oct", 9},
            {"Nov", 10},
            {"Dec", 11},
        };

        private static Dictionary<string, int> DayNumberMapping = new Dictionary<string, int>(){
            {"Sun", 0},
            {"Mon", 1},
            {"Tue", 2},
            {"Wed", 3},
            {"Thu", 4},
            {"Fri", 5},
            {"Sat", 6},
        };

        private delegate bool UnitConverter(string unit, out int result);

        private static UnitConverter DefaultConverter = int.TryParse;
        private static UnitConverter DayConverter = (string unit, out int result) =>
        {
            if (int.TryParse(unit, out result))
            {
                return true;
            }
            if (DayNumberMapping.TryGetValue(unit, out result))
            {
                return true;
            }

            result = -1;
            return false;
        };

        private static UnitConverter MonthConverter = (string unit, out int result) =>
        {
            if (int.TryParse(unit, out result))
            {
                return true;
            }
            if (MonthNumberMapping.TryGetValue(unit, out result))
            {
                return true;
            }
            result = -1;
            return false;
        };

        public const char UnitDelimiter = ' ';
        public const int MaxParts = 7;
        private TimeSpecificationParser()
        {

        }

        public static TimeSpecification Parse(string spec)
        {
            if (string.IsNullOrWhiteSpace(spec))
            {
                throw new Exception($"{nameof(spec)} cannot be null or emmpty");
            }

            var parts = spec.Split(UnitDelimiter);

            if (parts.Length == 0)
            {
                throw new Exception("Invalid spec, must contain at least one part");
            }

            if (parts.Length > MaxParts)
            {
                throw new Exception("Invalid spec, too many parts");
            }
            var parser = new TimeSpecificationParser();
            IUnit seconds = parser.GetSecondUnit(parts[0]);
            IUnit minutes = null;
            IUnit hours = null;
            IUnit days = null;
            IUnit months = null;
            IUnit years = null;

            if (parts.Length > 1)
            {
                minutes = parser.GetMinuteUnit(parts[1]);
            }

            if (parts.Length > 2)
            {
                hours = parser.GetHourUnit(parts[2]);
            }

            if (parts.Length > 3)
            {
                days = parser.GetDayUnit(parts[3]);
            }

            if (parts.Length > 4)
            {
                months = parser.GetMonthUnit(parts[4]);
            }

            if (parts.Length > 5)
            {
                years = parser.GetYearUnit(parts[5]);
            }

            return new TimeSpecification(seconds, minutes, hours, days, months, years);
        }

        public static bool TryParse(string spec, out TimeSpecification timeBlock)
        {
            try
            {
                timeBlock = Parse(spec);
                return true;
            }
            catch (Exception ex)
            {
                timeBlock = null;
                return false;
            }
        }

        private bool IsValidPart(string part)
        {
            return !string.IsNullOrWhiteSpace(part);
        }

        private bool IsEveryUnit(string part)
        {
            return part[0] == AlwaysUnit.Idenifier;
        }

        private bool IsRangeUnit(string part)
        {
            return part.Contains(RangeUnit.Idenifier);
        }

        private bool IsSpecificUnit(string part)
        {
            return part.Contains(SpecificUnit.Idenifier);
        }

        private bool IsIntervalUnit(string part)
        {
            return part.Contains(IntervalUnit.Idenifier);
        }


        private RangeUnit GetRangeUnit(string part, UnitConverter converter)
        {
            var units = part.Split(RangeUnit.Idenifier);

            if (units.Length != 2)
            {
                throw new Exception($"Invalid range specification length, '{part}'");
            }

            if (!converter(units[0], out int lower))
            {
                throw new Exception($"Invalid lower range unit value {lower}, '{part}'");
            }


            if (!converter(units[1], out int upper))
            {
                throw new Exception($"Invalid upper range unit value {upper}, '{part}'");
            }
            if (lower >= upper)
            {
                throw new Exception($"Invalid range, lower cannot be higher than the upper bounds, '{part}'");
            }

            return new RangeUnit(lower, upper);
        }

        private SpecificUnit GetSpecficUnit(string part, UnitConverter converter)
        {
            var units = part.Split(SpecificUnit.Idenifier);

            int[] specificUnits = new int[units.Length];
            for (int i = 0; i < units.Length; i++)
            {
                var unit = units[i];
                if (!converter(unit, out int unitValue))
                {
                    throw new Exception($"Invalid value {unit}  in part, '{part}'");
                }
                specificUnits[i] = unitValue;
            }
            return new SpecificUnit(specificUnits);

        }

        private SingleUnit GetSingleUnit(string part, UnitConverter converter)
        {
            if (!converter(part, out int value))
            {
                throw new Exception($"Invalid value {part}  in part, '{part}'");
            }

            return new SingleUnit(value);
        }

        private IntervalUnit GetIntervalUnit(string part, UnitConverter converter)
        {
            part = part.Replace(IntervalUnit.Idenifier.ToString(), "");

            if (!converter(part, out int unit))
            {
                throw new Exception($"Invalid value, '{part}'");
            }
            return new IntervalUnit(unit);
        }

        private IUnit GetUnit(string part, UnitConverter converter = null)
        {
            if (converter == null)
            {
                converter = DefaultConverter;
            }

            if (!IsValidPart(part))
            {
                throw new Exception("Invalid part, seconds");
            }

            if (IsEveryUnit(part))
            {
                return AlwaysUnit.Value;
            }

            if (IsRangeUnit(part))
            {
                return GetRangeUnit(part, converter);
            }

            if (IsSpecificUnit(part))
            {
                return GetSpecficUnit(part, converter);
            }

            if (IsIntervalUnit(part))
            {
                return GetIntervalUnit(part, converter);
            }

            return GetSingleUnit(part, converter);
        }

        private IUnit GetSecondUnit(string part)
        {
            return GetUnit(part);

        }

        private IUnit GetMinuteUnit(string part)
        {
            return GetUnit(part);
        }

        private IUnit GetHourUnit(string part)
        {
            return GetUnit(part);
        }

        private IUnit GetDayUnit(string part)
        {
            return GetUnit(part, DayConverter);
        }

        private IUnit GetMonthUnit(string part)
        {
            return GetUnit(part, MonthConverter);
        }

        private IUnit GetYearUnit(string part)
        {
            return GetUnit(part);
        }
    }
}