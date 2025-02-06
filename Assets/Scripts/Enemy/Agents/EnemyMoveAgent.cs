using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached
        {
            get { return this._isReached; }
        }

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isReached = false;
        }

        private void FixedUpdate()
        {
            SetEnemyPosition();
        }

        private void SetEnemyPosition()
        {
            if (this._isReached)
            {
                return;
            }

            var vector = this._destination - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this._isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}