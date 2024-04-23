namespace Game.Common.Stats
{
    public class MovementSpeed : ITypeStat<int>
    {
        private readonly string _id;
        private int _value;
    
        public MovementSpeed(string id, int value)
        {
            _id = id;
            _value = value;
        }

        public int GetValue() => _value;

        public void Add(int value)
        {
            _value += value;
        }

        public string Id() => _id;
    }
}