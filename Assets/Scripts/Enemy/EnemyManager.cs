using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IFireable
    {
        [SerializeField] private BulletConfig bulletConfig;

        [SerializeField] private EnemyPool enemyPool;

        [SerializeField] private BulletSystem bulletSystem;
        
        private readonly HashSet<Enemy> _activeEnemies = new();

        private int _secondsToWait = 1;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_secondsToWait);
                OnSpawn();
            }
        }

        private void OnSpawn()
        {
            if (enemyPool.TrySpawnEnemy(out var enemyObject))
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();

                if (_activeEnemies.Add(enemyObject.GetComponent<Enemy>()))
                {
                    enemy.OnHpEmpty += OnDestroyed;

                    enemy.OnFire += OnFire;
                }
            }
        }
        private void OnDestroyed(Enemy enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.OnHpEmpty -= OnDestroyed; 
                enemy.OnFire -= OnFire;

                enemyPool.UnspawnEnemy(enemy.transform.gameObject);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            Fire(position, direction, bulletConfig);
        }

        public void Fire(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                IsPlayer = false,
                PhysicsLayer = (int)bulletConfig.PhysicsLayer,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
                Position = position,
                Velocity = direction * bulletConfig.Speed
            });
        }
    }
}