using UnityEngine;

namespace Game.Core.Input
{
    public class KeyboardButtonInput : IButtonInput
    {
        private readonly KeyCode _keyCode;

        public KeyboardButtonInput(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public bool IsButtonDown() => UnityEngine.Input.GetKeyDown(_keyCode);
    }
}