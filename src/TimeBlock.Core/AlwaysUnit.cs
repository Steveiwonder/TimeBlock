namespace TimeBlock.Core
{
    public class AlwaysUnit : IUnit
    {
        public static AlwaysUnit Value => new AlwaysUnit();

        public const char Idenifier = '*';

        public bool IsMatch(int value)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}