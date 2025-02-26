using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour, IFireable
    {
        public bool FireRequired;

        [SerializeField] private GameManager gameManager;

        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;

        private IInputHandler _inputHandler;

        private GameObject _character;
        private MoveComponent _moveComponent;
        private WeaponComponent _weaponComponent;
        private HitPointsComponent _hitPointsComponent;
        private void Start()
        {
            _character = FindObjectOfType<Player>().gameObject;

            _moveComponent = _character.GetComponent<MoveComponent>();
            _weaponComponent = _character.GetComponent<WeaponComponent>();
            _hitPointsComponent = _character.GetComponent<HitPointsComponent>();
        }
        private void OnEnable()
        {
            if (_character == null) return;
            _hitPointsComponent.OnHpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            if (_character == null) return;
            _hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
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
            _moveComponent.MoveByRigidbodyVelocity(movement * Time.fixedDeltaTime);
        }

        private void HandleFireInput()
        {
            FireRequired = true;
        }

        private void CheckFireRequired()
        {
            if (FireRequired)
            {
                OnFlyBullet();
                FireRequired = false;
            }
        }
        private void OnFlyBullet()
        {
            var weapon = _weaponComponent;
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
                IsPlayer = true,
                PhysicsLayer = (int)bulletConfig.PhysicsLayer,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
                Position = position,
                Velocity = direction * bulletConfig.Speed
            });
        }
    }
}