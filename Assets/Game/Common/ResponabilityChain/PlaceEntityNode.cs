using Game.Positions;
using UnityEngine;

namespace Game.Common.ResponabilityChain
{
    public sealed class PlaceEntityNode<T> : AbstractNodeHandler<T> where T : MonoBehaviour
    {
        private readonly IPositionGenerator _positionGenerator;
    
        public PlaceEntityNode(IPositionGenerator positionGenerator)
        {
            _positionGenerator = positionGenerator;
        }
    
        public override T Handle(T obj)
        {
            obj.transform.position = _positionGenerator.GeneratePosition();
            return base.Handle(obj);
        }
    }
}