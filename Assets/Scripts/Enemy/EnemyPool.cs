using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;

        [SerializeField] private GameObject character;

        [SerializeField] private Transform worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform container;

        [SerializeField] private GameObject prefab;

        private readonly Queue<GameObject> _enemyPool = new();

        private int _maxEnemies = 7;
        
        private void Awake()
        {
            InstantiateEnemy(_maxEnemies);
        }

        public bool TrySpawnEnemy(out GameObject enemy)
        {
            enemy = null;

            if (!this._enemyPool.TryDequeue(out var dequeuedEnemy))
            {
                return false; 
            }

            dequeuedEnemy.transform.SetParent(this.worldTransform);

            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            dequeuedEnemy.transform.position = spawnPosition.position;

            var attackPosition = this.enemyPositions.RandomAttackPosition();
            dequeuedEnemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            dequeuedEnemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character);

            enemy = dequeuedEnemy;

            return true; 

        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.container);
            this._enemyPool.Enqueue(enemy);
        }

        public void InstantiateEnemy(int maxEnemies)
        {
            for (var i = 0; i < maxEnemies; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this._enemyPool.Enqueue(enemy);
            }
        }
    }
}