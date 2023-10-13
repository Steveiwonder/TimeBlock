
namespace TimeBlock.Core
{

    public class IntervalUnit : IUnit
    {
        public const char Idenifier = '/';
        private int _interval;
        public IntervalUnit(int interval)
        {
            _interval = interval;
        }

        public static IntervalUnit From(int value)
        {
            return new IntervalUnit(value);
        }

        public bool IsMatch(int value)
        {
            return value % _interval == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var unit = obj as IntervalUnit;
            return unit._interval == _interval;
        }

        public override int GetHashCode()
        {
            return 17 * 23 * _interval;
        }
    }
}