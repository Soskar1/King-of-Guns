using KingOfGuns.Core.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    public class Player : Entity
    {
        [SerializeField] private Gun _gun;
        private Rigidbody2D _rigidbody;
        private Input _input;
        private Jumping _jumping;
        private GroundCheck _groundCheck;

        protected override void Awake()
        {
            base.Awake();
            _input = ServiceLocator.Instance.Get<Input>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumping = GetComponent<Jumping>();
            _groundCheck = GetComponent<GroundCheck>();
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

        private void Jump(InputAction.CallbackContext context)
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            _gun.Shoot();
            Vector2 direction = (_gun.transform.rotation * Vector2.right).normalized;
            float forceX = -direction.x * _gun.KnockbackForce;
            float forceY = -direction.y * _gun.KnockbackForce;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + forceX, forceY);
        }
    }
}