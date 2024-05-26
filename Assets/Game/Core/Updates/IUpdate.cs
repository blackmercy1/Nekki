using System;

namespace Game.Core.Updates
{
    public interface IUpdate
    {
        void GameUpdate(float deltaTime);
        event Action<IUpdate> UpdateRemoveRequested;
    }
}