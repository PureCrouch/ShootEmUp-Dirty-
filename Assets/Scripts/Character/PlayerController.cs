using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : Player, IFireable
    {
        [SerializeField] private GameObject character; 

        [SerializeField] private GameManager gameManager;

        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;

        private IInputHandler _inputHandler;

        public bool fireRequired;

        private void Start()
        {
            moveComponent = character.GetComponent<MoveComponent>();
            weaponComponent = character.GetComponent<WeaponComponent>();
            hitPointsComponent = character.GetComponent<HitPointsComponent>();
        }
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
            HandleInput();
            CheckFireRequired();
        }

        private void HandleInput()
        {
            float horizontalDirection = _inputHandler.GetHorizontalMovement();
            HandleMovePlayer(horizontalDirection);

            if (_inputHandler.GetFireInput())
            {
                HandleFireInput();
            }
        }

        private void HandleMovePlayer(float horizontalDirection)
        {
            var movement = new Vector2(horizontalDirection, 0);
            moveComponent.MoveByRigidbodyVelocity(movement * Time.fixedDeltaTime);
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
            var weapon = weaponComponent;
            Fire(weapon.Position, weapon.Rotation * Vector3.up, bulletConfig);
        }

        public void SetInputHandler(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
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