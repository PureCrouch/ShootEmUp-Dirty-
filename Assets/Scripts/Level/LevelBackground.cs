using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IFixedUpdatable
    {
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;

        private Transform _myTransform;

        [SerializeField] private Params _params;

        private void Awake()
        {
            SetBackgroundParams();
        }

        public void CustomFixedUpdate()
        {
            MoveBackground();
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float m_startPositionY;

            [SerializeField] public float m_endPositionY;

            [SerializeField] public float m_movingSpeedY;
        }

        private void SetBackgroundParams()
        {
            startPositionY = _params.m_startPositionY;
            endPositionY = _params.m_endPositionY;
            movingSpeedY = _params.m_movingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void MoveBackground()
        {
            if (_myTransform.position.y <= endPositionY)
            {
                _myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

                _myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * Time.fixedDeltaTime,
                positionZ
            );
        }
    }
}