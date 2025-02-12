using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IFixedUpdatable
    {
        public bool IsReached
        {
            get { return _isReached; }
        }

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        private void Awake()
        {
            FindObjectOfType<FixedUpdateController>().RegisterFixedUpdatable(this);
        }
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void CustomFixedUpdate()
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
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }

    }
}