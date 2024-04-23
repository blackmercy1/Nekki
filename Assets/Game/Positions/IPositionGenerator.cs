using UnityEngine;

namespace Game.Positions
{
    public interface IPositionGenerator
    {
        Vector3 GeneratePosition();
    }
}