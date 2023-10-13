namespace TimeBlock.Core
{
    // S M D M Y
    public class TimeBlock
    {

        public TimeBlock(
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
            && Days.IsMatch(dateTime.Day)
            && Months.IsMatch(dateTime.Month)
            && Years.IsMatch(dateTime.Year);
        }
    }


}