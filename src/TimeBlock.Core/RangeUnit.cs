namespace TimeBlock.Core
{
    public class RangeUnit : IUnit
    {
        public const char Idenifier = '-';
        private int _upper { get; }
        private int _lower { get; }

        public int Upper => _upper;
        public int Lower => _lower;

        public RangeUnit(int lower, int upper)
        {
            if (upper < lower)
                throw new ArgumentException("Upper bound must be greater than lower bound");

            _upper = upper;
            _lower = lower;
        }

        public RangeUnit(int value) : this(value, value)
        {

        }

        public bool IsMatch(int value)
        {
            return value >= _lower && value <= _upper;
        }

        public static RangeUnit From(int upper, int lower)
        {
            return new RangeUnit(upper, lower);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var unit = obj as RangeUnit;
            return unit._lower == _lower && unit._upper == _upper;
        }

        public override int GetHashCode()
        {
            return 17 * 23 * _lower * _upper;
        }
    }
}