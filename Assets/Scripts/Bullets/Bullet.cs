using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;

        [NonSerialized] public int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Vector2 _pauseVelocity = Vector2.zero;
        private Vector2 _resumeVelocity;
        public void SetBulletArgs(BulletArgs bulletArgs)
        {
            SetPosition(bulletArgs.position);
            SetColor(bulletArgs.color);
            SetPhysicsLayer(bulletArgs.physicsLayer);
            SetVelocity(bulletArgs.velocity);
            damage = bulletArgs.damage;
            isPlayer = bulletArgs.isPlayer;
        }
        private void Awake()
        {
            FindObjectOfType<GameStateController>().RegisterGameStateListener(this);
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

       public void StartGame()
        {
            //rigidbody2D.isKinematic = false;
        }

        public void PauseGame()
        {
            _resumeVelocity = rigidbody2D.velocity;
            rigidbody2D.isKinematic = true;
            rigidbody2D.velocity = _pauseVelocity;
        }

        public void ResumeGame()
        {
            rigidbody2D.isKinematic = false;
            rigidbody2D.velocity = _resumeVelocity;
        }
    }
}