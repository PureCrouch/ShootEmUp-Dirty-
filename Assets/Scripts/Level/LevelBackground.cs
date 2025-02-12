using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;

        private Transform myTransform;

        [SerializeField] private Params myParams;

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
            [SerializeField] public float startPositionY;

            [SerializeField] public float endPositionY;

            [SerializeField] public float movingSpeedY;
        }

        private void SetBackgroundParams()
        {
            startPositionY = myParams.startPositionY;
            endPositionY = myParams.endPositionY;
            movingSpeedY = myParams.movingSpeedY;
            myTransform = transform;
            var position = myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void MoveBackground()
        {
            if (myTransform.position.y <= endPositionY)
            {
                myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

                myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * Time.fixedDeltaTime,
                positionZ
            );
        }
    }
}