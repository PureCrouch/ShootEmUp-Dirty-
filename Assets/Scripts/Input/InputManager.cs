using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public static event Action<float> OnMoveInput;
        public static event Action OnFireInput;

        private int _inputValue = 1;

        private void Update()
        {
            FireInput();
            MoveInput();
        }

        private void FireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireInput?.Invoke(); 
            }
        }
        private void MoveInput()
        {
            float horizontalDirection = 0;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalDirection = -_inputValue;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontalDirection = _inputValue;
            }

            OnMoveInput?.Invoke(horizontalDirection); 
        }
    }
}