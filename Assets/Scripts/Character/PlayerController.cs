using UnityEngine;
using UnityEngine.Windows;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour, IFireable, IUserInputListener
    {
        [SerializeField] private GameObject character; 

        [SerializeField] private GameManager gameManager;

        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;

        public bool fireRequired;

        private void OnEnable()
        {
            if (character.TryGetComponent(out HitPointsComponent hitPointsComponent))
            {
                hitPointsComponent.OnHpEmpty += OnCharacterDeath;
            }
        }

        private void OnDisable()
        {
            if (character.TryGetComponent(out HitPointsComponent hitPointsComponent))
            { 
                hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();

        private void FixedUpdate()
        {
            CheckFireRequired();
        }

        public void UserInputReceived(int input)
        {
            HandleMovePlayer(input);
        }
        public void FireInputReceived() 
        {
            HandleFireInput();
        }

        private void HandleMovePlayer(float horizontalDirection)
        {
            var movement = new Vector2(horizontalDirection, 0);
            character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(movement * Time.fixedDeltaTime);
        }

        private void HandleFireInput()
        {
            fireRequired = true;
        }

        private void CheckFireRequired()
        {
            if (fireRequired)
            {
                OnFlyBullet();
                fireRequired = false;
            }
        }
        private void OnFlyBullet()
        {
            var weapon = character.GetComponent<WeaponComponent>();
            Fire(weapon.Position, weapon.Rotation * Vector3.up, bulletConfig);
        }

        public void Fire(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = position,
                velocity = direction * bulletConfig.speed
            });
        }

    }
}