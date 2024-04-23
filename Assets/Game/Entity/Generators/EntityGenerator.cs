using System;
using Game.Common.Configs.Entity.Warrior;
using Game.Common.ResponabilityChain;
using Game.Common.Timer;
using Game.Core.Input.Updates;
using Game.Entity.Creators.Warrior;
using Game.Positions;
using UnityEngine;

//можно сделать generic и тогда класс не будет привязан к warrior
namespace Game.Entity.Generators
{
    public sealed class EntityGenerator : IDisposable
    {
        private readonly RandomLoopTimer _timer;
        private readonly GameArea _gameArea;
        private readonly GameUpdates _gameUpdates;
        private readonly WarriorEntityCreator _warriorEntityCreator;

        public EntityGenerator(
            RandomLoopTimer timer,
            GameArea gameArea,
            GameUpdates gameUpdates,
            WarriorConfig warriorConfig,
            Transform targetTransform)
        {
            _timer = timer;
            _gameArea = gameArea;
            _gameUpdates = gameUpdates; 
        
            _timer.TimIsUp += SpawnWarrior;
            _timer.Resume();

            _warriorEntityCreator = new WarriorEntityCreator(warriorConfig, targetTransform);
        }
    
        private void SpawnWarrior()
        {
            var entity = _warriorEntityCreator.CreateEntity();
            var handler = SpawnWarriorHandler();
            handler.Handle(entity);
        }

        private IHandler<Warrior> SpawnWarriorHandler()
        {
            var startHandler = new StartNode<Warrior>();
            var updateHandler = new AddToUpdateNode<Warrior>(_gameUpdates);
            var positionHandler = new PlaceEntityNode<Warrior>(_gameArea);

            startHandler
                .SetNext(updateHandler)
                .SetNext(positionHandler);
        
            return startHandler;
        }

        public void Dispose()
        {
            _timer.TimIsUp -= SpawnWarrior;
            _timer?.Dispose();
            _gameUpdates?.Dispose();
        }
    }
}