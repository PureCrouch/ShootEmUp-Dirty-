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

        private int _maxEnemies;
        
        private void Awake()
        {
            InstantiateEnemy();
        }

        public GameObject SpawnEnemy()
        {
            if (!this._enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(this.worldTransform);

            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.container);
            this._enemyPool.Enqueue(enemy);
        }

        public void InstantiateEnemy()
        {
            for (var i = 0; i < _maxEnemies; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this._enemyPool.Enqueue(enemy);
            }
        }
    }
}