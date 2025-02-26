using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShootEmUp.EnemyAttackAgent;



namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent), typeof(EnemyAttackAgent))]
    public class Enemy : Unit
    {
        public event Action<Enemy> OnHpEmpty;
        public event FireHandler OnFire;

        protected EnemyAttackAgent enemyAttackAgent;

        private void Awake()
        {
            hitPointsComponent = GetComponent<HitPointsComponent>();
            enemyAttackAgent = GetComponent<EnemyAttackAgent>();

            hitPointsComponent.OnHpEmpty += DoOnHpEmpty;
            enemyAttackAgent.OnFire += HandleFire;

            moveComponent = GetComponent<MoveComponent>();
        }

        private void DoOnHpEmpty(GameObject obj)
        {
            if (obj.TryGetComponent<Enemy>(out var enemy))
            {
                OnHpEmpty?.Invoke(enemy);
            }
        }

        private void HandleFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            OnFire?.Invoke(enemy, position, direction);
        }

    }
}

