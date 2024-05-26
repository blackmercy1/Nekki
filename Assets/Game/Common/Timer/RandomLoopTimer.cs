using System;
using Game.Common.Ranges;
using Game.Core.Updates;

namespace Game.Common.Timer
{
    public class RandomLoopTimer : IUpdate
    {
        public event Action TimIsUp;
        public event Action<IUpdate> UpdateRemoveRequested;

        private FloatRange _intervalRange;
        private float _currentInterval;
        private float _passedTime;
        private bool _isStopped;

        public RandomLoopTimer(FloatRange intervalRange)
        {
            _intervalRange = intervalRange;
            _currentInterval = _intervalRange.GetRandomValue();
        }

        public void Start()
        {
            _isStopped = false;
        }

        public void Stop()
        {
            _isStopped = true;
            UpdateRemoveRequested?.Invoke(this);
        }

        public void GameUpdate(float deltaTime)
        {
            if (_isStopped)
                return;

            _passedTime += deltaTime;

            if (_passedTime < _currentInterval)
                return;

            Reset();
        }

        private void Reset()
        {
            _passedTime = 0;
            _currentInterval = _intervalRange.GetRandomValue();
            TimIsUp?.Invoke();
        }
    }
}