using KingOfGuns.Core.Collectibles;
using KingOfGuns.Core.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Jumping))]
    public class Player : Entity
    {
        [SerializeField] private Gun _gun;
        private Rigidbody2D _rigidbody;
        private Input _input;
        private Jumping _jumping;

        protected override void Awake()
        {
            base.Awake();
            _input = ServiceLocator.Instance.Get<Input>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumping = GetComponent<Jumping>();
        }

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += Jump;
            _input.Controls.Player.Shoot.performed += Shoot;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= Jump;
            _input.Controls.Player.Shoot.performed -= Shoot;
        }

        public void FixedUpdate() => Move(Vector2.right * _input.MovementInput);

        private void Jump(InputAction.CallbackContext context) => _jumping.Jump();

        private void Shoot(InputAction.CallbackContext context)
        {
            if (_gun.IsReloading)
                return;
            
            _gun.Shoot();
            ApplyKnockback();
        }

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
                _gun.InstantReloading();
            }
        }
    }
}