using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

            if (!_enemyPool.TryDequeue(out var dequeuedEnemy))
            {
                return false; 
            }

            dequeuedEnemy.transform.SetParent(worldTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            dequeuedEnemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.RandomAttackPosition();

            if (dequeuedEnemy.TryGetComponent(out EnemyMoveAgent moveAgent))
            {
                moveAgent.SetDestination(attackPosition.position);
            }

            if (dequeuedEnemy.TryGetComponent(out EnemyAttackAgent attackAgent))
            {
                attackAgent.SetTarget(character);
            }

            enemy = dequeuedEnemy;

            return true; 
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(container);
            _enemyPool.Enqueue(enemy);
        }

        public void InstantiateEnemy(int maxEnemies)
        {
            for (var i = 0; i < maxEnemies; i++)
            {
                var enemy = Instantiate(prefab, container);
                _enemyPool.Enqueue(enemy);
            }
        }
    }
}