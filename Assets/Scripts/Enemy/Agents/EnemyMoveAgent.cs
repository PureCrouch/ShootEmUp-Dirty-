using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {

        private float _magnitudeBorder = 0.25f;
        public bool IsReached
        {
            get { return _isReached; }
        }

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            SetEnemyPosition();
        }

        private void SetEnemyPosition()
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= _magnitudeBorder)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}