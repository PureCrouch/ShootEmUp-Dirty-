using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;

        [SerializeField] private Bullet prefab;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            InstantiateBullet();
        }

        private void InstantiateBullet()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(prefab, container);
                _bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            m_cache.Clear();
            m_cache.AddRange(_activeBullets);

            CheckBulletsInBounds();
        }

        private void CheckBulletsInBounds()
        {
            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                var bullet = m_cache[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletArgs args)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(worldTransform);
            }
            else
            {
                bullet = Instantiate(prefab, worldTransform);
            }
            
            bullet.SetBulletArgs(args);
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(container);
                _bulletPool.Enqueue(bullet);
            }
        }
        
    }
}