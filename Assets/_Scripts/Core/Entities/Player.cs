using KingOfGuns.Core.Collectibles;
using KingOfGuns.Core.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Jumping))]
    [RequireComponent(typeof(Flipping))]
    public class Player : Entity, IReloadable
    {
        [SerializeField] private Gun _gun;
        private Transform _currentSpawnPoint;
        private Rigidbody2D _rigidbody;
        private Input _input;
        private Jumping _jumping;
        private Flipping _flipping;

        protected override void Awake()
        {
            base.Awake();
            _input = ServiceLocator.Instance.Get<Input>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumping = GetComponent<Jumping>();
            _flipping = GetComponent<Flipping>();
        }

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += Jump;
            _input.Controls.Player.Shoot.performed += Shoot;
            _input.Controls.Player.GunReload.performed += ReloadGun;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= Jump;
            _input.Controls.Player.Shoot.performed -= Shoot;
            _input.Controls.Player.GunReload.performed -= ReloadGun;
        }

        public void FixedUpdate()
        {
            if ((_flipping.FacingRight && _input.MovementInput < 0) ||
                (!_flipping.FacingRight && _input.MovementInput > 0))
                _flipping.Flip();

            Move(Vector2.right * _input.MovementInput);
        }

        public void SetSpawnPoint(Transform spawnPoint) => _currentSpawnPoint = spawnPoint;

        private void Jump(InputAction.CallbackContext context) => _jumping.Jump();

        private void Shoot(InputAction.CallbackContext context)
        {
            if (_gun.IsReloading)
                return;
            
            _gun.Shoot();

            if (!_gun.IsReloading)
                ApplyKnockback();
        }

        private void ReloadGun(InputAction.CallbackContext context) => _gun.StartReloading();

        private void ApplyKnockback()
        {
            Vector2 direction = (_gun.transform.rotation * Vector2.right).normalized;
            float forceX = -direction.x * _gun.KnockbackForce;
            float forceY = -direction.y * _gun.KnockbackForce;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + forceX, forceY);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ShotgunShell shotgunShell))
            {
                shotgunShell.Deactivate();
                _gun.InstantReload(1);
            }
        }

        public void Reload()
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = _currentSpawnPoint.position;
            _gun.InstantReload(_gun.MaxAmmo);
            _jumping.ReturnToInitialState();
        }
    }
}