using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
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
            if (this.fireRequired)
            {
                this.OnFlyBullet();
                this.fireRequired = false;
            }
        }

        private void HandleMovePlayer(float horizontalDirection)
        {
            var movement = new Vector2(horizontalDirection, 0);
            character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(-movement * Time.fixedDeltaTime);
        }
        private void HandleFireInput()
        {
            fireRequired = true;
        }
        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int) this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this.bulletConfig.speed
            });
        }
    }
}