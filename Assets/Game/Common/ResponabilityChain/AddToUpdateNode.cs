using Game.Core.Input.Updates;
using UnityEngine;

namespace Game.Common.ResponabilityChain
{
    public class AddToUpdateNode<T> : AbstractNodeHandler<T> where T : MonoBehaviour
    {
        private readonly GameUpdates _gameUpdates;

        public AddToUpdateNode(GameUpdates gameUpdates)
        {
            _gameUpdates = gameUpdates;
        }
    
        public override T Handle(T obj)
        {
            if (obj.TryGetComponent<IUpdate>(out var updatable)) 
                _gameUpdates.Add(updatable);
            return base.Handle(obj);
        }
    }
}