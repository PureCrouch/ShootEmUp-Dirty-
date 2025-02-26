using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool IsPlayer;

        [NonSerialized] public int Damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetBulletArgs(BulletArgs bulletArgs)
        {
            SetPosition(bulletArgs.Position);
            SetColor(bulletArgs.Color);
            SetPhysicsLayer(bulletArgs.PhysicsLayer);
            SetVelocity(bulletArgs.Velocity);
            Damage = bulletArgs.Damage;
            IsPlayer = bulletArgs.IsPlayer;
        }
        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}