using System;

namespace Game.Core.Input.Updates
{
    public interface IUpdate
    {
        void GameUpdate(float deltaTime);
        event Action<IUpdate> UpdateRemoveRequested;
    }
}