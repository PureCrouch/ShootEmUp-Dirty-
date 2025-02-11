using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent weaponComponent;

        [SerializeField] private EnemyMoveAgent moveAgent;

        [SerializeField] private float countdown;

        private GameObject _target;
        private float _currentTime;

        public void SetTarget(GameObject target)
        {
            this._target = target;
        }

        public void Reset()
        {
            this._currentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            CheckEnemyMovement();
            CheckPlayerHitpointsExist();
            CheckEnemyFireReload();
        }

        private void CheckEnemyMovement()
        {
            if (!this.moveAgent.IsReached)
            {
                return;
            }
        }

        private void CheckPlayerHitpointsExist()
        {
            if (!this._target.TryGetComponent(out HitPointsComponent hitPointsComponent) || !hitPointsComponent.IsHitPointsExists())
            {
                return;
            }
        }

        private void CheckEnemyFireReload()
        {
            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                this.SetFireOptions();
                this._currentTime += this.countdown;
            }
        }
        private void SetFireOptions()
        {
            var startPosition = this.weaponComponent.Position;
            var vector = (Vector2) this._target.transform.position - startPosition;
            var direction = vector.normalized;
            this.OnFire?.Invoke(this.gameObject, startPosition, direction);
        }
    }
}