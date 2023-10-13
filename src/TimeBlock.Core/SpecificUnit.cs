namespace TimeBlock.Core
{
    public class SpecificUnit : IUnit
    {
        public const char Idenifier = ',';
        private int[] _units;
        public SpecificUnit(int[] units)
        {
            _units = units ?? Array.Empty<int>();
        }

        public bool IsMatch(int value)
        {
            return _units.Contains(value);
        }
        public static object From(params int[] units)
        {
            return new SpecificUnit(units);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var unit = obj as SpecificUnit;
            return unit._units.Order().SequenceEqual(_units.Order());
        }

        public override int GetHashCode()
        {
            return 17 * 23 * _units.Aggregate((a, b) => a * b);
        }
    }
}