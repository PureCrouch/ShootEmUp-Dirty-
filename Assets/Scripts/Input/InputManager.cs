using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IInputHandler
    {
        private bool _fireInput = false;

        private int _inputValue = 1;

        private IUserInputListener[] _userInputListeners;

        private void Awake()
        {
            _userInputListeners = transform.parent.GetComponentsInChildren<IUserInputListener>();
        }
        private void Update()
        {
            FireInput();
            GetHorizontalInput();
        }

        public void FireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NotifyFireInput();
                _fireInput = true;
            }
        }

        public void GetHorizontalInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                NotifyMoveInput(-_inputValue);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                NotifyMoveInput(_inputValue);
            }
        }
        private void NotifyFireInput()
        {
            foreach (var listener in _userInputListeners)
            {
                listener.FireInputReceived(); 
            }
        }

        private void NotifyMoveInput(int direction)
        {
            foreach (var listener in _userInputListeners) 
            {
                listener.UserInputReceived(direction);
            }

        }
        public bool GetFireInput()
        {
            bool fireInput = _fireInput;
            _fireInput = false;
            return fireInput;
        }

    }
}