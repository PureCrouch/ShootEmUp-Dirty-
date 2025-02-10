using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour, IFireable
    {
        [SerializeField] private GameObject character; 

        [SerializeField] private GameManager gameManager;

        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;

        [SerializeField] private LevelBounds levelBounds;

        public bool fireRequired;

        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;

            InputManager.OnMoveInput += HandleMovePlayer;
            InputManager.OnFireInput += HandleFireInput;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;

            InputManager.OnMoveInput -= HandleMovePlayer;
            InputManager.OnFireInput -= HandleFireInput;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        private void FixedUpdate()
        {
            CheckFireRequired();
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
        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            Fire(weapon.Position, weapon.Rotation * Vector3.up, bulletConfig);
        }

        private void CheckFireRequired()
        {
            if (this.fireRequired)
            {
                this.OnFlyBullet();
                this.fireRequired = false;
            }
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