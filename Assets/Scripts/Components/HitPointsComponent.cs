using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpEmpty;

        [SerializeField] private int hitPoints;
        
        public bool IsHitPointsExists() 
        {
            return this.hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this.hitPoints -= damage;

            if (this.hitPoints <= 0)
            {
                this.OnHpEmpty?.Invoke(this.gameObject);
            }
        }
    }
}