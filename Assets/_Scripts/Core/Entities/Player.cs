using KingOfGuns.Core.Collectibles;
using KingOfGuns.Core.Guns;
using KingOfGuns.Core.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Jumping))]
    [RequireComponent(typeof(Flipping))]
    public class Player : Entity
    {
        [SerializeField] private GunHandler _gunHandler;
        private Rigidbody2D _rigidbody;
        private Input _input;
        private Jumping _jumping;
        private Flipping _flipping;

        public void Initialize(Input input, Gun gun, AmmoUI ammoUI)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumping = GetComponent<Jumping>();
            _flipping = GetComponent<Flipping>();

            _input = input;
            _input.Controls.Player.Jump.performed += Jump;
            _input.Controls.Player.Shoot.performed += Shoot;
            _input.Controls.Player.GunReload.performed += ReloadGun;

            _gunHandler.Initialize(gun, ammoUI);

            Health.OnDie += () => gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (_input is null)
                return;

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

        private void Jump(InputAction.CallbackContext context) => _jumping.Jump();
        private void Shoot(InputAction.CallbackContext context) => _gunHandler.Shoot();
        private void ReloadGun(InputAction.CallbackContext context) => _gunHandler.ReloadGun();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ShotgunShell shotgunShell))
            {
                shotgunShell.Disable();
                _gunHandler.InstantReload(1);
            }
        }

        public override void Reset()
        {
            base.Reset();
            _rigidbody.velocity = Vector2.zero;
            _jumping.ReturnToInitialState();
            _gunHandler.Reset();
            gameObject.SetActive(true);
        }
    }
}