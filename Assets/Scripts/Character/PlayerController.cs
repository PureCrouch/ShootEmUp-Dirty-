using UnityEngine;
using UnityEngine.Windows;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour, IFireable, IUserInputListener, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener, IFixedUpdatable
    {
        [SerializeField] private GameObject character;

        [SerializeField] private GameStateController gameStateController;

        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;

        public bool fireRequired;

        private bool _canMove;
        private bool _canFire;

        private void OnEnable()
        {

            if (character == null) return;
            
            if (character.TryGetComponent(out HitPointsComponent hitPointsComponent))
            {
                hitPointsComponent.OnHpEmpty += OnCharacterDeath;
            }
            
        }

        private void OnDisable()
        {
            if (character == null) return;
            
            if (character.TryGetComponent(out HitPointsComponent hitPointsComponent))
            {
                hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
            }
            
        }

        private void OnCharacterDeath(GameObject _) => gameStateController.FinishGame();

        public void CustomFixedUpdate()
        {
            CheckFireRequired();
        }

        public void UserInputReceived(int input)
        {
            HandleMovePlayer(input);
        }
        public void FireInputReceived() 
        {
            if (_canFire)
                HandleFireInput();
        }

        private void HandleMovePlayer(float horizontalDirection)
        {
            if (!_canMove) 
                return;

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

        public void StartGame()
        {
            _canMove = true;
            _canFire = true;
        }

        public void PauseGame()
        {
            _canMove = false;
            _canFire = false;
        }

        public void ResumeGame()
        {
            _canMove = true;
            _canFire = true;
        }
        public void FinishGame()
        {
            _canMove = false;
            _canFire = false;
        }
    }
}