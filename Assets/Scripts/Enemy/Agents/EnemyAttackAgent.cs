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
            _target = target;
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        private void FixedUpdate()
        {
            CheckEnemyMovement();
            CheckPlayerHitpointsExist();
            CheckEnemyFireReload();
        }

        private void CheckEnemyMovement()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }
        }

        private void CheckPlayerHitpointsExist()
        {
            if (!_target.TryGetComponent(out HitPointsComponent hitPointsComponent) || !hitPointsComponent.IsHitPointsExists())
            {
                return;
            }
        }

        private void CheckEnemyFireReload()
        {
            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                SetFireOptions();
                _currentTime += countdown;
            }
        }

        private void SetFireOptions()
        {
            var startPosition = weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(gameObject, startPosition, direction);
        }
    }
}