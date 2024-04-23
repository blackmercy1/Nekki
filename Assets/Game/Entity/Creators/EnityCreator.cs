namespace Game.Entity.Creators
{
    public abstract class EnityCreator<T>
    {
        public abstract T CreateEntity();
    }
}