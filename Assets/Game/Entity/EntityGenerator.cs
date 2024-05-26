using Game.Common.Timer;
using UnityEngine;

namespace Game.Entity
{
    public sealed class EntityGenerator<T> where T : MonoBehaviour
    {
        private readonly RandomLoopTimer _timer;
        private readonly EntityCreator<T> _entityCreator;

        public EntityGenerator(
            EntityCreator<T> entityCreator,
            RandomLoopTimer timer)
        {
            _entityCreator = entityCreator;
            _timer = timer;
        }

        public void Start()
        {
            _timer.TimIsUp += SpawnEntity;
            _timer.Start();
        }
        
        //on game over
        public void Stop()
        {
            _timer.TimIsUp -= SpawnEntity;
            _timer.Stop();
        }

        private void SpawnEntity()
        {
            var entity = _entityCreator.CreateEntity();
        }
    }
}