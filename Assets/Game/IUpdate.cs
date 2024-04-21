using System;

public interface IUpdate
{
    void GameUpdate(float deltaTime);
        
    event Action<IUpdate> UpdateRemoveRequested;
}