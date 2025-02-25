using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShootEmUp.EnemyAttackAgent;



namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent), typeof(EnemyAttackAgent))]
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnHpEmpty;
        public event FireHandler OnFire;

        private HitPointsComponent _hpComponent;
        private EnemyAttackAgent _enemyAttackAgent;

        private void Awake()
        {
            _hpComponent = GetComponent<HitPointsComponent>();
            _enemyAttackAgent = GetComponent<EnemyAttackAgent>();

            _hpComponent.OnHpEmpty += HpComponentOnHpEmpty;
            _enemyAttackAgent.OnFire += HandleFire; 

        }

        private void HpComponentOnHpEmpty(GameObject obj)
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

