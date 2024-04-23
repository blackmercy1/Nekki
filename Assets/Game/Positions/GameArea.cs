using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Positions
{
    public sealed class GameArea : IPositionGenerator
    { 
        private readonly Transform _leftTop;
        private readonly Transform _rightTop;
        private readonly Transform _leftDown;
        private readonly Transform _rightDown;
    
        private readonly Vector2 _halfSize;
    
        private List<Transform> _borders;
    
        public GameArea(
            Transform leftTop,
            Transform rightTop, 
            Transform leftDown, 
            Transform rightDown)
        {
            _leftTop = leftTop;
            _rightTop = rightTop;
            _leftDown = leftDown;
            _rightDown = rightDown;
        
            InitList();
        }

        private void InitList()
        {
            _borders = new List<Transform>
            {
                _leftTop,
                _rightTop,
                _leftDown,
                _rightDown
            };
        }

        public Vector3 GeneratePosition() => GetRandomStartPosition(); 
    
        private Vector3 GetRandomStartPosition()
        {
            if (_borders.Count == 0)
                InitList();
        
            var randomValue = Random.Range(0, _borders.Count - 1);
            var startPosition = GetPosition(_borders[randomValue]);

            if (_borders.Count != 0)
                _borders.Remove(_borders[randomValue]);
            
            return startPosition;
        }

        private Vector3 GetPosition(Transform transform)
        {
            // if (transform == _leftTop || transform == _rightTop)
            // {
            //     var newPosition = GetBorderYPosition(transform);
            //     return newPosition;
            // }
            //
            // if (transform == _leftDown || transform == _rightDown)
            // {
            //     var newPosition = GetBorderXPosition(transform);
            //     return newPosition;
            // }
            //
            // throw new NotImplementedException("Все сломалось");

            if (transform == _leftTop)
            {
                var spawnPosition = Vector3.Lerp(_leftTop.position, _leftDown.position, .5f);
                return spawnPosition;
            }
            if (transform == _leftDown)
            {
                var spawnPosition = Vector3.Lerp(_leftDown.position, _rightDown.position, .5f);
                return spawnPosition;
            }
            if (transform == _rightDown)
            {
                var spawnPosition = Vector3.Lerp(_rightDown.position, _rightTop.position, .5f);
                return spawnPosition;
            }
            if (transform == _rightTop)
            {
                var spawnPosition = Vector3.Lerp(_rightTop.position, _leftTop.position, .5f);
                return spawnPosition;
            }

            return Vector3.down;
        }

        private Vector2 GetBorderXPosition(Transform transform)
        {
            var randomX = Random.Range(-_halfSize.x , _halfSize.x);

            var newPosition = new Vector2(randomX, transform.position.y);
            
            return newPosition;
        }

        private Vector2 GetBorderYPosition(Transform transform)
        {
            var randomY = Random.Range(-_halfSize.x , _halfSize.y);

            var newPosition = new Vector2(transform.position.x, randomY);

            return newPosition;
        }

        public void PlaceObject(Transform transform, Vector2 startPosition) => transform.position = startPosition;
    }
}