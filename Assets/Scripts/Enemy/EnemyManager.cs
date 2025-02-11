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
        
        private readonly HashSet<GameObject> _activeEnemies = new();

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
            var enemy = this.enemyPool.SpawnEnemy();
            if (enemy != null)
            {
                if (this._activeEnemies.Add(enemy))
                {
                    enemy.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnDestroyed;
                    enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                }
            }
        }
        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                enemyPool.UnspawnEnemy(enemy);
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
                isPlayer = false,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = position,
                velocity = direction * bulletConfig.speed
            });
        }
    }
}