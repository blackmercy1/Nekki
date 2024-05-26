namespace Game.Entity
{
    public abstract class EntityCreator<T>
    {
        public abstract T CreateEntity();
    }
}