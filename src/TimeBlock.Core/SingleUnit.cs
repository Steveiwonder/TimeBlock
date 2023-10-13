
namespace TimeBlock.Core
{
    public class SingleUnit : IUnit
    {
        private int _value;
        public SingleUnit(int value)
        {
            _value = value;
        }

        public bool IsMatch(int value)
        {
            return _value == value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var unit = obj as SingleUnit;
            return unit._value == _value;
        }

        public override int GetHashCode()
        {
            return 17 * 23 * _value;
        }

        public static SingleUnit From(int value)
        {
            return new SingleUnit(value);
        }
    }
}