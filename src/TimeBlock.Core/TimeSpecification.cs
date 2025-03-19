namespace TimeBlock.Core
{
    // S M D M Y
    public class TimeSpecification
    {

        public TimeSpecification(
            IUnit seconds = null,
            IUnit minutes = null,
            IUnit hours = null,
            IUnit days = null,
            IUnit months = null,
            IUnit years = null
           )
        {
            Seconds = seconds ?? AlwaysUnit.Value;
            Minutes = minutes ?? AlwaysUnit.Value;
            Hours = hours ?? AlwaysUnit.Value;
            Days = days ?? AlwaysUnit.Value;
            Months = months ?? AlwaysUnit.Value;
            Years = years ?? AlwaysUnit.Value;
        }

        public IUnit Seconds { get; }
        public IUnit Minutes { get; }
        public IUnit Hours { get; }
        public IUnit Days { get; }
        public IUnit Months { get; }
        public IUnit Years { get; }


        public bool IsMatch(DateTime dateTime)
        {
            return Seconds.IsMatch(dateTime.Second)
            && Minutes.IsMatch(dateTime.Minute)
            && Hours.IsMatch(dateTime.Hour)
            && Days.IsMatch((int)dateTime.DayOfWeek)
            && Months.IsMatch(dateTime.Month-1)
            && Years.IsMatch(dateTime.Year);
        }

        public override string ToString()
        {
            return $"{FormatUnit(Seconds)} {FormatUnit(Minutes)} {FormatUnit(Hours)} " +
                   $"{FormatUnit(Days)} {FormatUnit(Months)} {FormatUnit(Years)}";
        }

        private string FormatUnit(IUnit unit) => unit switch
        {
            AlwaysUnit => "*",
            RangeUnit r => $"{r.Lower}-{r.Upper}",
            SingleUnit s => s.Value.ToString(),
            SpecificUnit sp => string.Join(",", sp.Units),
            _ => throw new NotImplementedException($"Unknown unit type: {unit.GetType()}")
        };
    }


}