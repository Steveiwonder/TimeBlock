
namespace TimeBlock.Core
{
    public class SingleUnit : IUnit
    {
        private int _value;
        public int Value => _value;
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
            return obj is SingleUnit unit && unit._value == _value;
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public static SingleUnit From(int value)
        {
            return new SingleUnit(value);
        }
    }
}