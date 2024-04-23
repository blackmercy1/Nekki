namespace Game.Common.Stats
{
    public class Defence : ITypeStat<int>
    {
        private readonly string _id;
        private int _value;
    
        public Defence(string id, int value)
        {
            _value = value;
            _id = id;
        }

        public int GetValue() => _value;

        public void Add(int value)
        {
            _value += value;
        }

        public string Id()
        {
            return _id;
        }
    }
}