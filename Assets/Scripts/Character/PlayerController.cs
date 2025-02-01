using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool fireRequired;

        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            InputManager.OnFireInput += HandleFireInput;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
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
        private void HandleFireInput()
        {
            fireRequired = true;
        }
        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}