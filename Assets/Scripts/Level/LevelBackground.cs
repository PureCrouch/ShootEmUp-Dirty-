using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        private Transform _myTransform;

        [SerializeField] private Params _params;

        private void Awake()
        {
            SetBackgroundParams();
        }

        private void FixedUpdate()
        {
            MoveBackground();
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float StartPositionY;

            [SerializeField] public float EndPositionY;

            [SerializeField] public float MovingSpeedY;
        }

        private void SetBackgroundParams()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void MoveBackground()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

                _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }
    }
}