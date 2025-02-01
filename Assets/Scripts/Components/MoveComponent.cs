using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;

        [SerializeField]
        private bool isPlayer;

        private void OnEnable()
        {
            InputManager.OnMoveInput += HandleMoveInput; 
        }

        private void OnDisable()
        {
            InputManager.OnMoveInput -= HandleMoveInput; 
        }
        private void HandleMoveInput(float horizontalDirection)
        {
            if (isPlayer) 
            {
                Move(horizontalDirection);
            }
        }
        private void Move(float horizontalDirection)
        {
            var movement = new Vector2(horizontalDirection, 0) * speed * Time.fixedDeltaTime;
            var nextPosition = rigidbody2D.position + movement;
            rigidbody2D.MovePosition(nextPosition);
        }

        /* private void HandleFireInput()
        {
            if (isPlayer)
            {
                characterController._fireRequired = true;
            }
        }
        */
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}