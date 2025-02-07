using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;

        [NonSerialized] public int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetBulletArgs(BulletArgs bulletArgs)
        {
            SetPosition(bulletArgs.position);
            SetColor(bulletArgs.color);
            SetPhysicsLayer(bulletArgs.physicsLayer);
            SetVelocity(bulletArgs.velocity);
            damage = bulletArgs.damage;
            isPlayer = bulletArgs.isPlayer;
        }
        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetVelocity(Vector2 velocity)
        {
            this.rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        private void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }
    }
}