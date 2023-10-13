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
            Seconds = seconds;
            Minutes = minutes;
            Hours = hours;
            Days = days;
            Months = months;
            Years = years;
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