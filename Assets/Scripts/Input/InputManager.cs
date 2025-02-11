using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IInputHandler
    {
        private bool _fireInput = false;

        private int _inputValue = 1;

        private void Update()
        {
            FireInput();
        }

        public void FireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fireInput = true;
            }
        }

        public float GetHorizontalMovement()
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

            return horizontalDirection;
        }

        public bool GetFireInput()
        {
            bool fireInput = _fireInput;
            _fireInput = false;
            return fireInput;
        }

    }
}