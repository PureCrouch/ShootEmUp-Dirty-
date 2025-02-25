using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IFixedUpdatable, IStartGameListener
    {
        [SerializeField] private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;

        [SerializeField] private Bullet prefab;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public void StartGame()
        {
            InstantiateBullet();
        }
        private void InstantiateBullet()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                _bulletPool.Enqueue(bullet);
            }
        }
        public void CustomFixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            CheckBulletsInBounds();
        }

        private void CheckBulletsInBounds()
        {
            for (int i = 0, count = this._cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
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
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }
            
            bullet.SetBulletArgs(args);
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
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