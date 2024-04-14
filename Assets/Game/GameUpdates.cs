using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameUpdates : MonoBehaviour, IDisposable
{
    private List<IUpdate> _updates;
    private bool _isStoppedFlag = false;
    
    public void AddToUpdateList(IUpdate gameUpdate)
    {
        _updates ??= new List<IUpdate>();
        _updates.Add(gameUpdate);
        gameUpdate.UpdateRemoveRequested += OnUpdateRemoveRequested;
    }

    private void OnUpdateRemoveRequested(IUpdate gameUpdate)
    {
        gameUpdate.UpdateRemoveRequested -= OnUpdateRemoveRequested;
        RemoveFromUpdateList(gameUpdate);
    }

    private void RemoveFromUpdateList(IUpdate gameUpdate)
    {
        var index = _updates.FindIndex(s => s == gameUpdate);
        var lastIndex = _updates.Count - 1;
        _updates[index] = _updates[lastIndex];
        _updates.RemoveAt(lastIndex);
    }

    private void Update()
    {
        if (_isStoppedFlag)
            return;

        for (var i = 0; i < _updates.Count; i++)
        {
            _updates[i].GameUpdate(Time.deltaTime);
        }
    }

    public void StopUpdate()
    {
        _isStoppedFlag = true;
    }

    public void ResumeUpdate()
    {
        _isStoppedFlag = false;
    }

    public void Dispose()
    {
        _updates.ForEach(gameUpdate =>
        {
            gameUpdate.UpdateRemoveRequested -= OnUpdateRemoveRequested;
        });
    }
}